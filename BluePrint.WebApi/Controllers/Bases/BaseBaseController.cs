using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BluePrint.WebApi.Controllers.Bases;

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
                CancellationToken ct = default(CancellationToken));
        }

        public abstract class WithoutResponse : BaseController
        {
            protected WithoutResponse(IMediator mediator) : base(mediator)
            {
            }

            public abstract Task<ActionResult> HandleAsync(
                TRequest request,
                CancellationToken ct = default(CancellationToken));
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
                CancellationToken ct = default(CancellationToken));
        }

        public abstract class WithoutResponse : BaseController
        {
            protected WithoutResponse(IMediator mediator) : base(mediator)
            {
            }

            public abstract Task<ActionResult> HandleAsync(
                CancellationToken ct = default(CancellationToken));
        }
    }
}