using System;
using System.Globalization;
using System.Net;
using System.Text;
using Geocoding.Yandex.Enums;

namespace Geocoding.Yandex
{
    internal class UrlGenerator
    {
        public UrlGenerator(string baseUrl, string apiKey)
        {
            BaseUrl = baseUrl;
            ApiKey = apiKey;
        }

        private string ApiKey { get; }
        private string BaseUrl { get; }
        public string GenerateGeocodeUrl(string geocode, Bounds bounds = null, Language? language = null, int? skip = null, int? results = null)
        {
            var builder = GenerateBaseUrl(geocode, bounds: bounds, language: language, skip: skip, results: results);
            return builder.ToString();
        }
        
        public string GenerateGeocodeReverseUrl(string geocode, Kind? kind = null, Bounds bounds = null, Language? language = null, int? skip = null, int? results = null)
        {
            var builder = GenerateBaseUrl(geocode, bounds: bounds, language: language, skip: skip, results: results);
            if (kind.HasValue)
            {
                builder.Append("&kind=");
                switch (kind.Value)
                {
                    case Enums.Kind.District:
                        builder.Append(WebUtility.UrlEncode("district"));
                        break;
                    case Enums.Kind.House:
                        builder.Append(WebUtility.UrlEncode("house"));
                        break;
                    case Enums.Kind.Street:
                        builder.Append(WebUtility.UrlEncode("street"));
                        break;
                    case Enums.Kind.Metro:
                        builder.Append(WebUtility.UrlEncode("metro"));
                        break;
                    case Enums.Kind.Locality:
                        builder.Append(WebUtility.UrlEncode("locality"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return builder.ToString();
        }

        private StringBuilder GenerateBaseUrl(string geocode, Bounds bounds = null, Language? language = null, int? skip = null, int? results = null)
        {
            var builder = new StringBuilder();
            
            builder.Append($"{BaseUrl}?geocode={WebUtility.UrlEncode(geocode)}&format=json");
            builder.Append($"&apikey={ApiKey}");
            
            if (language.HasValue)
            {
                builder.Append("&lang=");
                switch (language.Value)
                {
                    case Enums.Language.Russian:
                        builder.Append(WebUtility.UrlEncode("ru_RU"));
                        break;
                    case Enums.Language.Belarusian:
                        builder.Append(WebUtility.UrlEncode("be_BY"));
                        break;
                    case Enums.Language.Ukrainian:
                        builder.Append(WebUtility.UrlEncode("uk_UA"));
                        break;
                    case Enums.Language.Turkish:
                        builder.Append(WebUtility.UrlEncode("tr_TR"));
                        break;
                    case Enums.Language.EnglishRussianFeatures:
                        builder.Append(WebUtility.UrlEncode("en_RU"));
                        break;
                    case Enums.Language.EnglishAmericanFeatures:
                        builder.Append(WebUtility.UrlEncode("en_US"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (bounds != null)
            {
                builder.Append("&rspn=1&bbox=");
                builder.Append(
                    $"{bounds.SouthWest.Longitude.ToString(CultureInfo.InvariantCulture)}," +
                    $"{bounds.SouthWest.Latitude.ToString(CultureInfo.InvariantCulture)}~" +
                    $"{bounds.NorthEast.Longitude.ToString(CultureInfo.InvariantCulture)}," +
                    $"{bounds.NorthEast.Latitude.ToString(CultureInfo.InvariantCulture)}");
            }
                
            if (skip.HasValue)
            {
                builder.Append($"&skip={skip.Value}");
            }

            if (results.HasValue)
            {
                builder.Append($"&results={results.Value}");
            }

            return builder;
        }
    }
}