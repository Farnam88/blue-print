using BluePrint.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BluePrint.Application.Common.Behaviour;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Unhandled Exception for {Name}", typeof(TRequest).Name);
            throw;
        }
    }
}