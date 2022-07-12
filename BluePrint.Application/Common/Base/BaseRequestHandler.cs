using MediatR;
using IUnitOfWork = BluePrint.Application.Data.IUnitOfWork;

namespace BluePrint.Application.Common.Base;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly Data.IUnitOfWork Uow;

    protected BaseRequestHandler(Data.IUnitOfWork uow)
    {
        Uow = uow;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}