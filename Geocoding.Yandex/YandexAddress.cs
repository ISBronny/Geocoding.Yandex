namespace Geocoding.Yandex
{

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class YandexAddress : Address
    {
        public YandexAddress(string formattedAddress, Location coordinates, string provider) : base(formattedAddress, coordinates, provider)
        {
            
        }
    }
}