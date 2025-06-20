using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Extensions;
using FLP.Core.Context.Constants;
using FLP.Core.Exceptions;
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

public class UpdateBubFunction(ILogger<UpdateBubFunction> _logger, IMediator _mediator)
{
    [Function(nameof(UpdateBubFunction))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateBugRequest), Required = true, Example = typeof(UpdateBugRequestExample))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(UpdateBugResponse), statusCode: HttpStatusCode.OK, Description = "The bug were Updated successfully.")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "put", "patch", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.", req);

        try
        {
            var request = await req.DeserializeRequestBodyAsync<UpdateBugRequest>();
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
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Bug not found.");
            return new NotFoundObjectResult("Bug not found.");
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while processing the request.", ex);
            return new BadRequestObjectResult("An error occurred while processing the request.");
        }
    }
}

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