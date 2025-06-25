using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Examples.Responses;
using FLP.AzureFunctions.Extensions;
using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace FLP.AzureFunctions.Functions.Bugs;

public class GetBugsPaginatedFunction(ILogger<GetBugsPaginatedFunction> _logger, IMediator _mediator)
{
    [Function(nameof(GetBugsPaginatedFunction))]
    [OpenApiParameter("Page", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page number for pagination")]
    [OpenApiParameter("PageSize", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page Size for pagination")]
    [OpenApiParameter("Query", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Query for search")]
    [OpenApiParameter("Status", In = ParameterLocation.Query, Required = false, Type = typeof(BugStatus), Description = "Bug Status for search")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BaseResponse<GetBugsPaginatedResponse>), Description = "The response message", Example = typeof(GetBugResponseExample))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a paginated bug request.", req);

        var request = req.GetBugsPaginatedRequest();

        var response = await _mediator.Send(request, cancellationToken);
        if (response is not null && response.IsSuccess)
            return new OkObjectResult(response);
        else
            return new BadRequestObjectResult(response);
    }


}