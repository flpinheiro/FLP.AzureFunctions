using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunctions.Examples.Responses;
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

        req.Query.TryGetValue("Page", out var page);
        req.Query.TryGetValue("PageSize", out var pageSize);
        req.Query.TryGetValue("Query", out var query);
        req.Query.TryGetValue("Status", out var status);

        var request = new GetBugsPaginatedRequest()
        {
            Page = ParseInt(page, 1),
            PageSize = ParseInt(pageSize, 10),
            Query = query,
            Status = ParseStatus(status),
        };

        var response = await _mediator.Send(request, cancellationToken);
        if (response is not null && response.IsSuccess)
            return new OkObjectResult(response);
        else
            return new BadRequestObjectResult(response);
    }

    private static int ParseInt(string? value, int defaultValue = 1)
    {
        if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var result))
        {
            return defaultValue;
        }
        return result;
    }

    private static BugStatus? ParseStatus(string? value)
    {
        if (string.IsNullOrEmpty(value) || !Enum.TryParse<BugStatus>(value, true, out var result))
        {
            return null; // Default status if parsing fails
        }
        return result;
    }
}