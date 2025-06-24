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

public class CreateBugFunction(ILogger<CreateBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(CreateBugFunction))]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateBugRequest), Required = true, Example = typeof(CreateBugRequestExample))]
    [OpenApiResponseWithBody(contentType: "application/json", bodyType: typeof(BaseResponse<GetBugByIdResponse>), statusCode: HttpStatusCode.OK, Description = "The bug were Created successfully.", Example = typeof(GetBugByIdResponseExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a create bug request.", req);

        var request = await req.DeserializeRequestBodyAsync<CreateBugRequest>(cancellationToken);
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
