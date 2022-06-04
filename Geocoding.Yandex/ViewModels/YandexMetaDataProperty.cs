using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexMetaDataProperty
    {
        [JsonProperty("GeocoderResponseMetaData")]
        public YandexGeocoderResponseMetaData GeocoderResponseMetaData { get; set; }

        [JsonProperty("GeocoderMetaData")] public YandexGeocoderMetaData GeocoderMetaData { get; set; }
    }
}