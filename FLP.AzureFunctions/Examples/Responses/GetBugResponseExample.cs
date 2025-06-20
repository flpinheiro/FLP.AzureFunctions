using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Shared;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Responses;

public class GetBugResponseExample : OpenApiExample<BaseResponse<GetBugsResponse>>
{
    public override IOpenApiExample<BaseResponse<GetBugsResponse>> Build(NamingStrategy namingStrategy = null)
    {
        var data = new GetBugsResponse(

            new List<GetBugResponse>
            {
                new GetBugResponse
                {
                    Id = Guid.NewGuid(),
                    Title = "Sample Bug Title",
                    Status = Core.Context.Constants.BugStatus.Open, // Example status,
                }
            },
            1
        );
        var success = new BaseResponse<GetBugsResponse>(data);
        var fail = new BaseResponse<GetBugsResponse>("An error occurred while retrieving the bugs.");
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bugs Response success", success, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bugs Response fail", fail, namingStrategy));
        return this;
    }
}