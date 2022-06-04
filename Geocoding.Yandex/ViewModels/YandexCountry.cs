using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexCountry
    {
        [JsonProperty("AddressLine")] public string AddressLine { get; set; }

        [JsonProperty("CountryNameCode")] public string CountryNameCode { get; set; }

        [JsonProperty("CountryName")] public string CountryName { get; set; }

        [JsonProperty("AdministrativeArea")] public YandexAdministrativeArea AdministrativeArea { get; set; }
    }
}