using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class SubAdministrativeArea
    {
        [JsonProperty("SubAdministrativeAreaName")]
        public string SubAdministrativeAreaName { get; set; }

        [JsonProperty("Locality")] public Locality Locality { get; set; }
    }
}