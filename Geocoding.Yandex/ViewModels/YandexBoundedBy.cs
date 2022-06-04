using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexBoundedBy
    {
        [JsonProperty("Envelope")] public YandexEnvelope Envelope { get; set; }
    }
}