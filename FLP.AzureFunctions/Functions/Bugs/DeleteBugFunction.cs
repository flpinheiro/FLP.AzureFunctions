using FLP.Application.Requests.Bugs;
using FLP.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FLP.AzureFunctions.Functions.Bugs;

public class DeleteBugFunction(ILogger<DeleteBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(DeleteBugFunction))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Bug/{id}")] HttpRequest req, Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to delete a bug with ID: {id}", id, req);

        var response = await _mediator.Send(new DeleteBugRequest(id), cancellationToken);


        _logger.LogInformation("Bug with ID: {id} deleted successfully.", id);
        if (response is not null && response.IsSuccess)
            return new OkObjectResult($"Bug with Id: {id} deleted successfully.");
        else
            return new BadRequestObjectResult(response);
    }
}