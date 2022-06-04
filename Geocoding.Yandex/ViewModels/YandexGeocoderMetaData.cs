using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexGeocoderMetaData
    {
        [JsonProperty("kind")] public string Kind { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("precision")] public string Precision { get; set; }

        [JsonProperty("Address")] public YandexAddressRaw Address { get; set; }

        [JsonProperty("AddressDetails")] public YandexAddressDetails AddressDetails { get; set; }
    }
}