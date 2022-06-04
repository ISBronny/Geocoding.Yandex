using Geocoding.Yandex.ViewModels;

namespace Geocoding.Yandex.Tests;

public class Tests : TestBase
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
}