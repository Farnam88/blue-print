using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.WebApi.Controllers.Bases
{
    [ApiController]
    [ProducesResponseType(typeof(ResultModel<object>), 500)]
    [ProducesResponseType(typeof(ResultModel<object>), 400)]
    [ProducesResponseType(typeof(ResultModel<object>), 404)]
    [ProducesResponseType(typeof(ResultModel<object>), 505)]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator MediatorHandler;


        protected BaseController(IMediator mediator)
        {
            Preconditions.CheckNull(mediator, nameof(IMediator));

            MediatorHandler = mediator;
        }
    }

    public static class BaseEndpointController
    {
        public static class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse> : BaseController
            {
                protected WithResponse(IMediator mediator) : base(mediator)
                {
                }

                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default(CancellationToken));
            }

            public abstract class WithoutResponse : BaseController
            {
                protected WithoutResponse(IMediator mediator) : base(mediator)
                {
                }

                public abstract Task<ActionResult> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default(CancellationToken));
            }
        }

        public static class WithoutRequest
        {
            public abstract class WithResponse<TResponse> : BaseController
            {
                protected WithResponse(IMediator mediator) : base(mediator)
                {
                }

                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    CancellationToken cancellationToken = default(CancellationToken));
            }

            public abstract class WithoutResponse : BaseController
            {
                protected WithoutResponse(IMediator mediator) : base(mediator)
                {
                }

                public abstract Task<ActionResult> HandleAsync(
                    CancellationToken cancellationToken = default(CancellationToken));
            }
        }
    }
}