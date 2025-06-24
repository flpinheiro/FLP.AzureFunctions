using FLP.Core.Context.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Responses;

public class BaseResponseExample : OpenApiExample<BaseResponse>
{
    public override IOpenApiExample<BaseResponse> Build(NamingStrategy? namingStrategy = default)
    {
        var fail = new BaseResponse(false,"Operation not completed successfully.");
        var success = new BaseResponse();
        Examples.Add(OpenApiExampleResolver.Resolve("Base Response success", success, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Base Response fail", fail, namingStrategy));
        return this;
    }
}