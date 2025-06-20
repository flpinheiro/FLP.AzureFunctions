using FLP.Application.Requests.Bugs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace FLP.AzureFunctions.Examples.Requests;

public class CreateBugRequestExample : OpenApiExample<CreateBugRequest>
{
    public override IOpenApiExample<CreateBugRequest> Build(NamingStrategy? namingStrategy = null)
    {
        var example = new CreateBugRequest("Sample Bug Title", "This is a sample description for the bug.", Guid.NewGuid());
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bug Request", example, namingStrategy));
        return this;
    }
}