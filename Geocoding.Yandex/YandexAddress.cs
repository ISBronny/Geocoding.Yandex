using System.Globalization;
using Geocoding.Yandex.ViewModels;

namespace Geocoding.Yandex
{
    public class YandexAddress : ParsedAddress
    {
        public string HouseNumber { get; set; }

        private YandexAddress(string formattedAddress, Location coordinates, string provider) : base(formattedAddress, coordinates, provider)
        {
            
        }

        internal static YandexAddress FromGeoObject(YandexGeoObject geoObject)
        {
            var address = new YandexAddress(
                geoObject.MetaDataProperty.GeocoderMetaData.Text,
                new Location(
                    double.Parse(geoObject.Point.Pos.Split()[1], CultureInfo.InvariantCulture),
                    double.Parse(geoObject.Point.Pos.Split()[0], CultureInfo.InvariantCulture)
                ),
                "Yandex"
            );

            var country = geoObject?.MetaDataProperty?.GeocoderMetaData?.AddressDetails?.Country;
            address.Country = country?.CountryName;
            address.State = country?.AdministrativeArea?.AdministrativeAreaName;
            Locality locality;
            if (country?.AdministrativeArea?.SubAdministrativeArea == null)
            {
                locality = country?.AdministrativeArea?.Locality;
                address.County = country?.AdministrativeArea?.AdministrativeAreaName;
            }
            else
            {
                locality = country?.AdministrativeArea?.SubAdministrativeArea?.Locality;
                address.County = country?.AdministrativeArea?.SubAdministrativeArea?.SubAdministrativeAreaName;
            }

            address.City = locality?.LocalityName;
            address.Street = locality?.Thoroughfare?.ThoroughfareName;
            address.HouseNumber = locality?.Thoroughfare?.Premise?.PremiseNumber;

            address.PostCode = geoObject?.MetaDataProperty?.GeocoderMetaData?.Address?.PostalCode;
            
            return address;
        }
    }
}