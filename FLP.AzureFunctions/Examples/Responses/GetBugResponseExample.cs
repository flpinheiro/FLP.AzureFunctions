using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Responses;

public class GetBugResponseExample : OpenApiExample<BaseResponse<GetBugsPaginatedResponse>>
{
    public override IOpenApiExample<BaseResponse<GetBugsPaginatedResponse>> Build(NamingStrategy? namingStrategy = default)
    {
        var data = new GetBugsPaginatedResponse(

            [
                new ()
                {
                    Id = Guid.NewGuid(),
                    Title = "Sample Bug Title",
                    Status = Core.Context.Constants.BugStatus.Open, // Example status,
                }
            ],
            1
        );
        var success = new BaseResponse<GetBugsPaginatedResponse>(data);
        var fail = new BaseResponse<GetBugsPaginatedResponse>(false,"An error occurred while retrieving the bugs.");
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bugs Response success", success, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bugs Response fail", fail, namingStrategy));
        return this;
    }
}