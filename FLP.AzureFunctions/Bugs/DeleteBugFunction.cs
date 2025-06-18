using FLP.Application.Requests.Bugs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FLP.AzureFunctions.Bugs;

public class DeleteBugFunction(ILogger<DeleteBugFunction> _logger, IMediator _mediator)
{
    [Function(nameof(DeleteBugFunction))]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Bug/{id}")] HttpRequest req, Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request to delete a bug with ID: {BugId}", id);

        try
        {
           await _mediator.Send(new DeleteBugRequest(id), cancellationToken);
        }
        catch (Exception)
        {
            _logger.LogError("bug sith ID: {id} not found", id);
            return new BadRequestObjectResult($"Bug with ID {id} not found or could not be deleted.");
        }

        _logger.LogInformation("Bug with ID: {BugId} deleted successfully.", id);
        return new OkObjectResult($"Bug with Id: {id} deleted with successfuly");
    }
}