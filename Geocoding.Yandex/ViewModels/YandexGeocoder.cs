using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Geocoding.Yandex.Enums;
using Newtonsoft.Json;

namespace Geocoding.Yandex.ViewModels
{
    public class YandexGeocoder : IGeocoder
{
    private string _apiKey;

    public YandexGeocoder(string apiKey)
    {
        ApiKey = apiKey;
    }
    
    public string ApiKey
    {
        get { return _apiKey; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ApiKey can not be null or empty");

            _apiKey = value;
        }
    }

    public Kind? Kind { get; set; } = null;
    public Language? Language { get; set; } = null;
    
    private string ServiceUrl
    {
        get
        {
            var builder = new StringBuilder();
            builder.Append("https://geocode-maps.yandex.ru/1.x?geocode={0}&format=json");
            builder.Append($"&apikey={ApiKey}");

            if (Kind.HasValue)
            {
                builder.Append("&kind=");
                switch (Kind.Value)
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
            
            if (Language.HasValue)
            {
                builder.Append("&lang=");
                switch (Language.Value)
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
            

            return builder.ToString();
        }
    }
    
    
    async Task<IEnumerable<Address>> IGeocoder.GeocodeAsync(string address)
    {
        return await GeocodeAsync(address);
    }

    async Task<IEnumerable<Address>> IGeocoder.GeocodeAsync(string street, string city, string state, string postalCode, string country)
    {
        return await GeocodeAsync(BuildAddress(street, city, state, postalCode, country));
    }

    async Task<IEnumerable<Address>> IGeocoder.ReverseGeocodeAsync(Location location)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<Address>> IGeocoder.ReverseGeocodeAsync(double latitude, double longitude)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<YandexAddress>> GeocodeAsync(string address, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (string.IsNullOrEmpty(address))
            throw new ArgumentNullException(nameof(address));

        var request = BuildWebRequest( WebUtility.UrlEncode(address));
        return await ProcessRequest(request, cancellationToken).ConfigureAwait(false);
    }

    private string BuildAddress(string street, string city, string state, string postalCode, string country)
    {
        return $"{street} {city}, {state} {postalCode}, {country}";
    }
    
    private async Task<IEnumerable<YandexAddress>> ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            using (var client = BuildClient())
            {
                return await ProcessWebResponse(await client.SendAsync(request, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    private async Task<IEnumerable<YandexAddress>> ProcessWebResponse(HttpResponseMessage response)
    {
        var res = JsonConvert.DeserializeObject<YandexRoot>(await response.Content.ReadAsStringAsync());
        var yandexGeoObjects = res.Response.GeoObjectCollection.FeatureMember.Select(x => x.GeoObject);
        return yandexGeoObjects.Select(
            x => new YandexAddress(
                x.MetaDataProperty.GeocoderMetaData.Text,
                new Location(
                    double.Parse(x.Point.Pos.Split()[0], CultureInfo.InvariantCulture),
                    double.Parse(x.Point.Pos.Split()[1], CultureInfo.InvariantCulture)
                    ),
                "Yandex"
                )
        );
    }
    
    private HttpClient BuildClient()
    {
        return new HttpClient();
    }
    private HttpRequestMessage BuildWebRequest(string value)
    {
        var url = string.Format(ServiceUrl, value);
        return new HttpRequestMessage(HttpMethod.Get, url);
    }
}
}

