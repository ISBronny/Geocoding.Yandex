using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Geocoding.Yandex.Tests;

public class TestBase
{
    private string _apiKey;

    public string ApiKey
    {
        get
        {
            if (!string.IsNullOrEmpty(_apiKey))
                return _apiKey;
            
            var str = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\appsettings.json"));
            _apiKey = (string)(JsonConvert.DeserializeObject(str) as dynamic)["ApiKey"];
            
            return _apiKey;
        }
    }
}