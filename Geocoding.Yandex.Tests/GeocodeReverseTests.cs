using Geocoding.Yandex.Enums;
using Geocoding.Yandex.ViewModels;

namespace Geocoding.Yandex.Tests;

public class GeocodeReverseTests : TestBase
{

    [Test]
    public async Task SimpleTest()
    {
        var yandexAddresses = (await new YandexGeocoder(ApiKey).ReverseGeocodeAsync(new Location(55.749425, 37.591189))).ToList();

        Assert.That(yandexAddresses, Is.All.Property(nameof(YandexAddress.FormattedAddress)).Not.Empty);
        Assert.That(yandexAddresses.Select(x=>x.Coordinates), Is.All.Property(nameof(Location.Latitude)).Not.Zero);
        Assert.That(yandexAddresses.Select(x=>x.Coordinates), Is.All.Property(nameof(Location.Longitude)).Not.Zero);
    }
    
    [Test]
    [TestCase(Language.Russian, "Россия, Москва, Большой Николопесковский переулок")]
    [TestCase(Language.Belarusian, "Россия, Масква, Большой Николопесковский переулок")]
    [TestCase(Language.Turkish, "Moskova, Bolshoy Nikolopeskovskiy pereulok, Rusya")]
    [TestCase(Language.Ukrainian, "Росія, Москва, Великий Миколопісковський провулок")]
    [TestCase(Language.EnglishAmericanFeatures, "Russian Federation, Moscow, Bolshoy Nikolopeskovsky Lane")]
    [TestCase(Language.EnglishRussianFeatures, "Russia, Moscow, Bolshoy Nikolopeskovsky Lane")]
    public async Task LanguageTest(Language language, string expected)
    {
        var geocoder = new YandexGeocoder(ApiKey)
        {
            Results = 1,
            Language = language,
        };
        var yandexAddress = (await geocoder.ReverseGeocodeAsync(new Location(55.749425, 37.591189))).First();

        Assert.That(yandexAddress.FormattedAddress, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(Kind.House, "Россия, Москва, улица Знаменка, 14/1")]
    [TestCase(Kind.District, "Россия, Москва, Центральный административный округ, район Арбат, 36-й квартал")]
    [TestCase(Kind.Locality, "Россия, Москва")]
    [TestCase(Kind.Metro, "Россия, Москва, Арбатско-Покровская линия, метро Арбатская")]
    [TestCase(Kind.Street, "Россия, Москва, улица Воздвиженка")]
    [TestCase(null, "Россия, Москва, Центральный административный округ, район Арбат, 36-й квартал")]
    public async Task KindTest(Kind? kind, string expected)
    {
        var geocoder = new YandexGeocoder(ApiKey)
        {
            Kind = kind,
            Results = 1
        };

        var address = (await geocoder.ReverseGeocodeAsync(new Location(55.752538, 37.602772))).First();
        Assert.That(address.FormattedAddress, Is.EqualTo(expected));
    }
}