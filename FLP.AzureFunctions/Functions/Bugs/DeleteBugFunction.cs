using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Examples.Responses;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FLP.AzureFunctions.Functions.Bugs;

public class DeleteBugFunction(ILogger<DeleteBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(DeleteBugFunction))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(BaseResponse), statusCode: HttpStatusCode.OK, Description = "The bug were Updated successfully.", Example = typeof(BaseResponseExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Bug/{id}")] HttpRequest req, Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to delete a bug with ID: {id}", id, req);

        var response = await _mediator.Send(new DeleteBugRequest(id), cancellationToken);

        _logger.LogInformation("Bug with ID: {id} deleted successfully.", id);
        if (response is not null && response.IsSuccess)
            return new OkObjectResult(response);
        else
            return new BadRequestObjectResult(response);
    }
}