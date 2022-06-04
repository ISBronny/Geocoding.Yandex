using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexEnvelope
    {
        [JsonProperty("lowerCorner")] public string LowerCorner { get; set; }

        [JsonProperty("upperCorner")] public string UpperCorner { get; set; }
    }
}