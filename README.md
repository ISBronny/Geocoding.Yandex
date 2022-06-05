# Geocoding.Yandex

[Geocoding.net](https://github.com/chadly/Geocoding.net "Geocoding.net") library extension for working with Yandex geocoder. Yandex geocoder works well with the geography of Russia, Ukraine, Belarus, Turkey and other close countries.

The API returns latitude/longitude coordinates and normalized address information. This can be used to perform address validation, real time mapping of user-entered addresses, distance calculations, and much more.

Get the API key [here](https://developer.tech.yandex.ru/services/ "here")

## Installing

```
PM> Install-Package Yandex.Geocoding
```
Avaliable on [NuGet](https://www.nuget.org/packages/Yandex.Geocoding)
## Usage Example

### Simple Example

```csharp
IGeocoder geocoder = new YandexGeocoder("yandex-api-key");
IEnumerable<Address> addresses = await geocoder.GeocodeAsync("Tverskaya 4");
Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: Россия, Москва, Тверская улица, 4
Console.WriteLine("Coordinates: " + addresses.First().Coordinates.Latitude + ", " + addresses.First().Coordinates.Longitude); //Coordinates: 55,758493, 37,613198
```
It can also be used to return address information from latitude/longitude coordinates (aka reverse geocoding):
```csharp
IGeocoder geocoder = new YandexGeocoder("yandex-api-key");
IEnumerable<Address> addresses = await geocoder.ReverseGeocodeAsync(38.8976777, -77.036517);
```

### Using Yandex Provider Specific Data
#### Find address by known city
```csharp
YandexGeocoder geocoder = new YandexGeocoder(ApiKey);
IEnumerable<YandexAddress> addresses = await geocoder.GeocodeAsync("Tverskaya 4");
var moscowAddress = addresses.First(address=>address.City == "Moscow");
Console.WriteLine($"Country: {moscowAddress.Country}, City: {moscowAddress.City}, Postal Code: {moscowAddress.PostCode}");
```
#### Specify response language
```csharp
var geocoder = new YandexGeocoder(ApiKey)
{
  Language = Language.Turkish
};
```
Available languages: Russian, Belarusian, Ukrainian, Turkish, English
#### Specify toponym **(Only for reverse geocoding)**
```csharp
geocoder.Kind = Kind.Metro;
```
Possible values: House, Street, Metro, District, Locality
#### Specify search boundaries
Ignored when `Kind` is set to District
```csharp
geocoder.Bounds = new Bounds(55.542312, 37.253144, 55.933075, 37.861323);
```
#### Specify the number of addresses in response
```csharp
geocoder.Results = 30;
```
By default - 10
#### Specify the number of skipped addresses
```csharp
geocoder.Skip = 4;
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
