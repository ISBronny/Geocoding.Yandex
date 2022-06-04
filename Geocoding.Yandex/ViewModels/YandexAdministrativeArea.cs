using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexAdministrativeArea
    {
        [JsonProperty("AdministrativeAreaName")]
        public string AdministrativeAreaName { get; set; }

        [JsonProperty("Locality")] public Locality Locality { get; set; }

        [JsonProperty("SubAdministrativeArea")]
        public SubAdministrativeArea SubAdministrativeArea { get; set; }
    }
}