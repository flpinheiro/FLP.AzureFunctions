using FLP.Core.Context.Shared;
using FLP.Core.Interfaces.Repository;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Exceptions;

internal class GlobalRequestExceptionHandler<TRequest, TResponse, TException>(
   ILogger<GlobalRequestExceptionHandler<TRequest, TResponse, TException>> _logger, IUnitOfWork _uow) : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : notnull
        where TResponse : BaseResponse, new()
        where TException : Exception
{
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        var ex = exception;

        _logger.LogError(ex, "Something went wrong while handling request of type {@requestType}", typeof(TRequest));

        var response = new TResponse
        {
            IsSuccess = false,
            Message = [ex.Message],
        };

        state.SetHandled(response);

        _uow.RollbackTransaction();

        return Task.CompletedTask;
    }
}