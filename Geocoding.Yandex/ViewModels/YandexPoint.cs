using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexPoint
    {
        [JsonProperty("pos")] public string Pos { get; set; }
    }
}