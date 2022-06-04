using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexFeatureMember
    {
        [JsonProperty("GeoObject")] public YandexGeoObject GeoObject { get; set; }
    }
}