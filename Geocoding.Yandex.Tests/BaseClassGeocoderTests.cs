namespace Geocoding.Yandex.Tests;

public class BaseClassGeocoderTests : TestBase
{
    [Test]
    public async Task BasicTest()
    {
        IGeocoder geocoder = new YandexGeocoder(ApiKey);
        var addresses = (await geocoder.GeocodeAsync("Тверская 7")).ToList();
        Assert.That(addresses, Is.All.Not.Null);
    }
    
    [Test]
    public async Task BasicReverseTest()
    {
        IGeocoder geocoder = new YandexGeocoder(ApiKey);
        var addresses = (await geocoder.ReverseGeocodeAsync(new Location(55.743405, 37.597185))).ToList();
        Assert.That(addresses, Is.All.Not.Null);
    }
}