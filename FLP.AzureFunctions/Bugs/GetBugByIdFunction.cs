using System.Net;
using FLP.Application.Responses.Bugs;
using FLP.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace FLP.AzureFunctions.Bugs;

public class GetBugByIdFunction(ILogger<GetBugByIdFunction> _logger, IMediator _mediator)
{
    [Function(nameof(GetBugByIdFunction))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(GetBugByIdResponse), statusCode: HttpStatusCode.OK, Description = "The bug details were retrieved successfully.")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Bug/{id}")] HttpRequest req, Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a Get Bug By Id request.");
        try
        {
            var response = await _mediator.Send(new Application.Requests.Bugs.GetBugByIdRequest(id), cancellationToken);
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
