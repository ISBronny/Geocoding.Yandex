using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexComponent
    {
        [JsonProperty("kind")] public string Kind { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}