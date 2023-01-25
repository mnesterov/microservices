using System.Text.Json;

namespace WebHttpAggregator.Serialization.Json;

public static class JsonSerializationHelper
{
    private static readonly JsonSerializerOptions _options;

    static JsonSerializationHelper()
    {
        _options = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance 
        };
    }

    public static string Serialize<T>(T source)
    {
        var result = JsonSerializer.Serialize(source, _options);
        return result;
    }

    public static T Deserialize<T>(string source)
    {
        var result = JsonSerializer.Deserialize<T>(source, _options);
        return result;
    }
}