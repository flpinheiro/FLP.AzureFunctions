using FLP.Application.Requests.Bugs;
using FLP.Core.Context.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace FLP.AzureFunctions.Extensions;

public static class HttpRequestExtensions
{
    private static readonly JsonSerializerOptions _options = new()
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

    public static GetBugsPaginatedRequest GetBugsPaginatedRequest(this HttpRequest req)
    {
        req.Query.TryGetValue("Page", out var page);
        req.Query.TryGetValue("PageSize", out var pageSize);
        req.Query.TryGetValue("Query", out var query);
        req.Query.TryGetValue("Status", out var status);
        req.Query.TryGetValue("Sort", out var sort);

        var (sortBy, sortOrder) = ParseSort(sort);
        var request = new GetBugsPaginatedRequest()
        {
            Page = ParseInt(page, 1),
            PageSize = ParseInt(pageSize, 10),
            Query = query,
            Status = ParseStatus(status),
            SortBy = sortBy,
            SortOrder = sortOrder,
        };

        return request;
    }

    private static int ParseInt(string? value, int defaultValue = 1)
    {
        if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var result))
        {
            return defaultValue;
        }
        return result;
    }

    public static BugStatus? ParseStatus(string? value)
    {
        if (string.IsNullOrEmpty(value) || !TryParseDescription<BugStatus>(value, out var result))
        {
            return null; // Default status if parsing fails
        }
        return result;
    }
    /// <summary>
    /// Part sort query string as ascending, descending and sortBy, if possible
    /// <example>
    /// <list type="number">
    ///     <item>
    ///         Ascending/Descending:
    ///         <list type="bullet">
    ///             <item>sort=asc(ascending)</item>
    ///             <item>sort=desc(descending)</item>
    ///         </list>
    ///     </item>
    ///     <item>
    ///         Field-Based Sorting:
    ///         <list type="bullet">
    ///             <item>sort=price_asc (price ascending)</item>
    ///             <item>sort=price_desc (price descending)</item>
    ///         </list>
    ///     </item>
    /// </list>
    /// </example>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>

    public static (string?, SortOrder) ParseSort(string? value)
    {
        var sortBy = string.Empty;
        var sortOrder = SortOrder.Ascending;
        if (value == null)
            return (sortBy, sortOrder);
        var sorting = value.Split('_');
        if (sorting.Length == 2 && TryParseDescription<SortOrder>(sorting[1], out sortOrder))
        {
            sortBy = sorting[0];
            return (sortBy, sortOrder);
        }
        if (sorting.Length == 1 && TryParseDescription<SortOrder>(sorting[0], out sortOrder))
        {
            return (sortBy, sortOrder); ;
        }
        else
        {
            sortBy = sorting[0];
            return (sortBy, sortOrder);
        }
    }


    private static bool TryParseDescription<TEnum>(string description, out TEnum result) where TEnum : struct, Enum
    {
        if(Enum.TryParse<TEnum>(description,out result)) 
            return true;

        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var attr = field.GetCustomAttribute<DescriptionAttribute>();
            if ((attr != null && attr.Description.Equals(description, StringComparison.OrdinalIgnoreCase)) ||
                field.Name.Equals(description, StringComparison.OrdinalIgnoreCase))
            {
                var fieldValue = field.GetValue(null);
                if (fieldValue is TEnum enumValue)
                {
                    result = enumValue;
                    return true;
                }
            }
        }

        result = default!;
        return false;
    }
}
