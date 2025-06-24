using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace FLP.AzureFunction.UnitTest.Fixtures;

internal static class HttpRequestFixture
{
    public static HttpRequest Generate<T>(T TClass)
    {
        var request = Generate();

        var body = JsonSerializer.Serialize(TClass);
        var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(body));

        request.Body = bodyStream;

        return request;
    }

    public static HttpRequest Generate()
    {
        var context = new DefaultHttpContext();
        var request = context.Request;

        return request;
    }
}
