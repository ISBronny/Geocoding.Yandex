using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Geocoding.Yandex.Enums;
using Geocoding.Yandex.ViewModels;
using Newtonsoft.Json;

namespace Geocoding.Yandex
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
        public int? Skip { get; set; } = null;
        public Bounds Bounds { get; set; }
        public int? Results { get; set; }
        
        async Task<IEnumerable<Address>> IGeocoder.GeocodeAsync(string address)
        {
            return await GeocodeAsync(address);
        }

        async Task<IEnumerable<Address>> IGeocoder.GeocodeAsync(string street, string city, string state,
            string postalCode, string country)
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

        public async Task<IEnumerable<YandexAddress>> GeocodeAsync(string address,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            var request = BuildWebGeocodeRequest(address);
            return await ProcessRequest(request, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<YandexAddress>> ReverseGeocodeAsync(Location location,  CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = BuildWebGeocodeReverseRequest($"{location.Longitude}, {location.Latitude}");
            return await ProcessRequest(request, cancellationToken).ConfigureAwait(false);
        }

        private string BuildAddress(string street, string city, string state, string postalCode, string country)
        {
            return $"{street} {city}, {state} {postalCode}, {country}";
        }

        private async Task<IEnumerable<YandexAddress>> ProcessRequest(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                using (var client = BuildClient())
                {
                    return await ProcessWebResponse(await client.SendAsync(request, cancellationToken)
                        .ConfigureAwait(false)).ConfigureAwait(false);
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
            return yandexGeoObjects.Select(YandexAddress.FromGeoObject);
        }

        private HttpClient BuildClient()
        {
            return new HttpClient();
        }

        private HttpRequestMessage BuildWebGeocodeRequest(string value)
        {
            var url = new UrlGenerator("https://geocode-maps.yandex.ru/1.x", ApiKey).GenerateGeocodeUrl(value, Bounds, Language, Skip, Results);
            return new HttpRequestMessage(HttpMethod.Get, url);
        }
        
        private HttpRequestMessage BuildWebGeocodeReverseRequest(string value)
        {
            var url = new UrlGenerator("https://geocode-maps.yandex.ru/1.x", ApiKey).GenerateGeocodeReverseUrl(value, Kind, Bounds, Language, Skip, Results);
            return new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}

