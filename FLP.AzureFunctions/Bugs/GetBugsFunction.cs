using System.Net;
using System.Threading.Tasks;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Constants;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FLP.AzureFunctions.Bugs;

public class GetBugsFunction(ILogger<GetBugsFunction> _logger, IMediator _mediator)
{
    [Function(nameof(GetBugsFunction))]
    [OpenApiParameter("Page", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page number for pagination")]
    [OpenApiParameter("PageSize", In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "Page Size for pagination")]
    [OpenApiParameter("Query", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Query for search")]
    [OpenApiParameter("Status", In = ParameterLocation.Query, Required = false, Type = typeof(BugStatus), Description = "Bug Status for search")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(GetBugsResponse), Description = "The response message")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Bug")] HttpRequest req, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a paginated bug request.");
        try
        {
            req.Query.TryGetValue("Page", out var page);
            req.Query.TryGetValue("PageSize", out var pageSize);
            req.Query.TryGetValue("Query", out var query);
            req.Query.TryGetValue("Status", out var status);

            var request = new GetBugsRequest()
            {
                Page = ParseInt(page, 1),
                PageSize = ParseInt(pageSize, 10),
                Query = query,
                Status = ParseStatus(status),
            };

            var response = await _mediator.Send(request, cancellationToken);

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while processing the request.", ex);
            return new BadRequestObjectResult("an error ocured while processing the request.");
        }
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