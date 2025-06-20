using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Responses;

public class GetBugByIdResponseExample : OpenApiExample<BaseResponse<GetBugByIdResponse>>
{
    public override IOpenApiExample<BaseResponse<GetBugByIdResponse>> Build(NamingStrategy namingStrategy = null)
    {
        var data = new GetBugByIdResponse
        {
            Id = Guid.NewGuid(),
            Title = "Sample Bug Title",
            Description = "This is a sample bug description.",
            AssignedToUserId = Guid.NewGuid(),
            Status = Core.Context.Constants.BugStatus.Open, // Example status,
            ResolvedAt = DateTime.Now,
        };
        var success = new BaseResponse<GetBugByIdResponse>(data);
        var fail = new BaseResponse<GetBugByIdResponse>("An error occurred while retrieving the bug.");
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bug Response success", success, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bug Response fail", fail, namingStrategy));
        return this;
    }
}

public class BaseResponseExample : OpenApiExample<BaseResponse>
{
    public override IOpenApiExample<BaseResponse> Build(NamingStrategy namingStrategy = null)
    {
        var fail = new BaseResponse("Operation not completed successfully.");
        var success = new BaseResponse();
        Examples.Add(OpenApiExampleResolver.Resolve("Base Response success", success, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Base Response fail", fail, namingStrategy));
        return this;
    }
}