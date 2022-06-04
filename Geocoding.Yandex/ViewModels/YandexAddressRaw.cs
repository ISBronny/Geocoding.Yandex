using System.Collections.Generic;
using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexAddressRaw 
    {
        [JsonProperty("country_code")] public string CountryCode { get; set; }
        [JsonProperty("postal_code")] public string PostalCode { get; set; }

        [JsonProperty("formatted")] public string Formatted { get; set; }

        [JsonProperty("Components")] public List<YandexComponent> Components { get; set; }
    }
}