using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexResponse
    {
        [JsonProperty("GeoObjectCollection")] public YandexGeoObjectCollection GeoObjectCollection { get; set; }
    }
}