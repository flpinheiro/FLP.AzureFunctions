using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace FLP.AzureFunctions.Extensions;

internal static class HttpRequestExtensions
{
    private static readonly JsonSerializerOptions _options = new ()
    {
        AllowTrailingCommas = true,
        AllowOutOfOrderMetadataProperties = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
        ReadCommentHandling = JsonCommentHandling.Skip,
    };
    public static async Task<T?> DeserializeRequestBodyAsync<T>(this HttpRequest req, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var body = req.Body;
        if (body == null)
        {
            return default;
        }
        using var reader = new StreamReader(body);
        var requestBody = await reader.ReadToEndAsync(cancellationToken);
        if (string.IsNullOrEmpty(requestBody))
        {
            return default;
        }
        var request = JsonSerializer.Deserialize<T>(requestBody, _options);
        return request;
    }
}
