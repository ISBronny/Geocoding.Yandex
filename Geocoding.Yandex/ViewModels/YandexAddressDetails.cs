using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexAddressDetails
    {
        [JsonProperty("Country")] public YandexCountry Country { get; set; }
    }
}