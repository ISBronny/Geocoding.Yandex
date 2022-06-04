using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexGeocoderResponseMetaData
    {
        [JsonProperty("request")] public string Request { get; set; }

        [JsonProperty("found")] public string Found { get; set; }

        [JsonProperty("results")] public string Results { get; set; }
    }
}