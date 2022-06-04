using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexPremise
    {
        [JsonProperty("PremiseName")] public string PremiseName { get; set; }
    }
}