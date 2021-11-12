using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestAssignment.Domain.Extensions;
using IUnitOfWork = TestAssignment.Application.Data.IUnitOfWork;

namespace TestAssignment.Application.Common.Base
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected readonly IUnitOfWork Uow;

        protected BaseRequestHandler(IUnitOfWork uow)
        {
            Preconditions.CheckNull(uow, nameof(IUnitOfWork));
            Uow = uow;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}