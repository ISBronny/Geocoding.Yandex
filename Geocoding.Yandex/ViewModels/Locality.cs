using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class Locality
    {
        [JsonProperty("LocalityName")] public string LocalityName { get; set; }

        [JsonProperty("Thoroughfare")] public Thoroughfare Thoroughfare { get; set; }

        [JsonProperty("Premise")] public YandexPremise Premise { get; set; }
    }
}