using System.Collections.Generic;
using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    internal class YandexGeoObjectCollection
    {
        [JsonProperty("metaDataProperty")] public YandexMetaDataProperty MetaDataProperty { get; set; }

        [JsonProperty("featureMember")] public List<YandexFeatureMember> FeatureMember { get; set; }
    }
}