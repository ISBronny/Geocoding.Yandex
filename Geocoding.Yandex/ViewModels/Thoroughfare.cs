using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class Thoroughfare
    {
        [JsonProperty("ThoroughfareName")] public string ThoroughfareName { get; set; }

        [JsonProperty("Premise")] public YandexPremise Premise { get; set; }
    }
}