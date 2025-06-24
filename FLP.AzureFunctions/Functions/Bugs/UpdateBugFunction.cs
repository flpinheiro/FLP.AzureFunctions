using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Examples.Requests;
using FLP.AzureFunctions.Examples.Responses;
using FLP.AzureFunctions.Extensions;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FLP.AzureFunctions.Functions.Bugs;

public class UpdateBugFunction(ILogger<UpdateBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(UpdateBugFunction))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateBugRequest), Required = true, Example = typeof(UpdateBugRequestExample))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(BaseResponse<GetBugByIdResponse>), statusCode: HttpStatusCode.OK, Description = "The bug were Updated successfully.", Example = typeof(GetBugByIdResponseExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "put", "patch", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.", req);

        var request = await req.DeserializeRequestBodyAsync<UpdateBugRequest>(cancellationToken);
        if (request == null)
        {
            _logger.LogError("Deserialization of request body failed.");
            return new BadRequestObjectResult(new BaseResponse(false, "Invalid request body."));
        }

        var response = await _mediator.Send(request, cancellationToken);
        if (response is not null && response.IsSuccess)
            return new OkObjectResult(response);
        else
            return new BadRequestObjectResult(response);

    }
}
