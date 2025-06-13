using FLP.Application.Requests.Bugs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Newtonsoft.Json.Serialization;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using FLP.AzureFunctions.Extensions;

namespace FLP.AzureFunctions.Bugs;

public class CreateBugFunction(ILogger<CreateBugFunction> _logger, IMediator _mediator)
{
    [Function("CreateBugFunction")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateBugRequest), Required = true, Example = typeof(CreateBugRequestExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var request = req.DeserializeRequestBody<CreateBugRequest>();
        if (request == null)
        {
            _logger.LogError("Deserialization of request body failed.");
            return new BadRequestObjectResult("Invalid request body.");
        }

        try
        {
            var response = await _mediator.Send(request, cancellationToken);
            return new OkObjectResult(response);
        }
        catch(ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error occurred while processing the request.");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred while processing the request.");
            return new BadRequestObjectResult("An error occurred while processing the request.");
        }
    }


}

public class CreateBugRequestExample : OpenApiExample<CreateBugRequest>
{
    public override IOpenApiExample<CreateBugRequest> Build(NamingStrategy? namingStrategy = null)
    {
        var example = new CreateBugRequest("Sample Bug Title", "This is a sample description for the bug.", Guid.NewGuid());
        Examples.Add(OpenApiExampleResolver.Resolve("Sample Bug Request", example, namingStrategy));
        return this;
    }
}