using FLP.Application.Requests.Bugs;
using FLP.Core.Context.Constants;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Requests;

public class UpdateBugRequestExample : OpenApiExample<UpdateBugRequest>
{
    public override IOpenApiExample<UpdateBugRequest> Build(NamingStrategy? namingStrategy = null)
    {
        var example = new UpdateBugRequest
        {
            Id = Guid.NewGuid(),
            Title = "Updated Bug Title",
            Description = "This is an updated description for the bug.",
            Status = BugStatus.InProgress,
            AssignedToUserId = Guid.NewGuid()
        };
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Update Bug Request", example, namingStrategy));
        return this;
    }
}