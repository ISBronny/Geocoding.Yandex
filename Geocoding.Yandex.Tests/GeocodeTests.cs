using Geocoding.Yandex.Enums;
using Geocoding.Yandex.ViewModels;

namespace Geocoding.Yandex.Tests;

public class GeocodeTests : TestBase
{

    [Test]
    [TestCase("Тверская 4")]
    public async Task SimpleTest(string address)
    {
        var yandexAddresses = (await new YandexGeocoder(ApiKey).GeocodeAsync(address)).ToList();

        Assert.That(yandexAddresses, Is.All.Property(nameof(YandexAddress.FormattedAddress)).Not.Empty);
        Assert.That(yandexAddresses.Select(x=>x.Coordinates), Is.All.Property(nameof(Location.Latitude)).Not.Zero);
        Assert.That(yandexAddresses.Select(x=>x.Coordinates), Is.All.Property(nameof(Location.Longitude)).Not.Zero);
    }
    
    [Test]
    [TestCase(Language.Russian, "Россия, Москва, Тверская улица, 4")]
    [TestCase(Language.Belarusian, "Россия, Масква, Цвярская вуліца, 4")]
    [TestCase(Language.Turkish, "Moskova, Tverskaya ulitsa, No:4, Rusya")]
    [TestCase(Language.Ukrainian, "Росія, Москва, Тверська вулиця, 4")]
    [TestCase(Language.EnglishAmericanFeatures, "Russian Federation, Moscow, Tverskaya Street, 4")]
    [TestCase(Language.EnglishRussianFeatures, "Russia, Moscow, Tverskaya Street, 4")]
    public async Task LanguageTest(Language language, string expected)
    {
        var geocoder = new YandexGeocoder(ApiKey)
        {
            Results = 1,
            Language = language
        };
        var yandexAddress = (await geocoder.GeocodeAsync("Тверская 4")).First();

        Assert.That(yandexAddress.FormattedAddress, Is.EqualTo(expected));
    }

    [Test]
    public async Task SkipTakeTest()
    {
        var geocoder = new YandexGeocoder(ApiKey)
        {
            Results = 5,
        };

        var addresses = (await geocoder.GeocodeAsync("Тверская 4")).ToList();

        for (int i = 1; i <= 4; i++)
        {
            geocoder.Results = 1;
            geocoder.Skip = i;
            var res = await geocoder.GeocodeAsync("Тверская 4");
            Assert.That(res.First().FormattedAddress, Is.EqualTo(addresses[i].FormattedAddress));
        }
    }
    
    [Test]
    [TestCase(55.542312, 37.253144,55.933075, 37.861323, 6)]
    [TestCase(55.242312, 37.253144,55.933075, 37.861323, 12)]
    [TestCase(55.242312, 36.253144,55.933075, 37.861323, 26)]
    public async Task BoundsTest(double x1, double y1, double x2, double y2, int count)
    {
        var geocoder = new YandexGeocoder(ApiKey)
        {
            Bounds = new Bounds(x1, y1, x2, y2), 
            Results = 30
        };

        var addresses = (await geocoder.GeocodeAsync("улица Кирова")).ToList();
        Assert.That(addresses, Has.Count.EqualTo(count));
    }
}