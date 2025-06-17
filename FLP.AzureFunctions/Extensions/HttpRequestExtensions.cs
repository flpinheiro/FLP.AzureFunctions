using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace FLP.AzureFunctions.Extensions;

internal static class HttpRequestExtensions
{
    public static async Task<T?> DeserializeRequestBodyAsync<T>(this HttpRequest req)
    {
        var body = req.Body;
        if (body == null)
        {
            return default;
        }
        using var reader = new StreamReader(body);
        var requestBody = await reader.ReadToEndAsync();
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            AllowOutOfOrderMetadataProperties = true,
            PropertyNameCaseInsensitive = true,
        };
        var request = JsonSerializer.Deserialize<T>(requestBody, options);
        return request;
    }
}
