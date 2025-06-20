using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace FLP.AzureFunctions.Bugs;

public class CreateBugFunction(ILogger<CreateBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(CreateBugFunction))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateBugRequest), Required = true, Example = typeof(CreateBugRequestExample))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(CreateBugReponse), statusCode: HttpStatusCode.OK, Description = "The bug were Created successfully.")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a create bug request.", req);
        try
        {
            var request = await req.DeserializeRequestBodyAsync<CreateBugRequest>();
            if (request == null)
            {
                _logger.LogError("Deserialization of request body failed.");
                return new BadRequestObjectResult("Invalid request body.");
            }

            var response = await _mediator.Send(request, cancellationToken);
            return new OkObjectResult(response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Validation error occurred while processing the request.");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while processing the request.", ex);
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