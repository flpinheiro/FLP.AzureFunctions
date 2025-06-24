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

public class GetBugByIdFunction(ILogger<GetBugByIdFunction> _logger, IMediator _mediator)
{
    [Function(nameof(GetBugByIdFunction))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(BaseResponse<GetBugByIdResponse>), statusCode: HttpStatusCode.OK, Description = "The bug details were retrieved successfully.", Example = typeof(GetBugByIdResponseExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Bug/{id}")] HttpRequest req, Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a Get Bug By Id request.", req);

        var response = await _mediator.Send(new Application.Requests.Bugs.GetBugByIdRequest(id), cancellationToken);

        if (response is not null && response.IsSuccess)
            return new OkObjectResult(response);
        else
            return new BadRequestObjectResult(response);

    }
}
