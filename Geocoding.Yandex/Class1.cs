namespace Geocoding.Yandex;

public class YandexGeocoder : IGeocoder
{
    public Task<IEnumerable<Address>> GeocodeAsync(string address)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Address>> GeocodeAsync(string street, string city, string state, string postalCode, string country)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Address>> ReverseGeocodeAsync(Location location)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Address>> ReverseGeocodeAsync(double latitude, double longitude)
    {
        throw new NotImplementedException();
    }
}