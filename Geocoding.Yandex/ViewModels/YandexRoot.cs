using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexRoot
    {
        [JsonProperty("response")] public YandexResponse Response { get; set; }
    }
}