using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexGeoObject
    {
        [JsonProperty("metaDataProperty")] public YandexMetaDataProperty MetaDataProperty { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("boundedBy")] public YandexBoundedBy BoundedBy { get; set; }

        [JsonProperty("Point")] public YandexPoint Point { get; set; }
    }
}