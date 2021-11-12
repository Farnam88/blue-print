using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestAssignment.Application.Contexts.TestAssignmentServices.Commands;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Domain.Common.Data;
using TestAssignment.WebApi.Controllers.Bases;

namespace TestAssignment.WebApi.Controllers.TestAssignmentEndpoints
{
    [Route(EndpointsBaseRouteNames.TestAssignment)]
    public class CreateTestAssignment : BaseEndpointController.WithRequest<CreateTestAssignmentCommand>.WithResponse<
        ResultModel<int>>
    {
        public CreateTestAssignment(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [SwaggerOperation(
            Tags = new[] {EndpointTagNames.TestAssignment},
            OperationId = "test-assignment.Create",
            Summary = "Add test assignments",
            Description = "Create test assignments"
        )]
        [ProducesResponseType(typeof(ResultModel<IList<TestAssignmentDto>>), 200)]
        public override async Task<ActionResult<ResultModel<int>>> HandleAsync(CreateTestAssignmentCommand request,
            [FromRoute] CancellationToken ct = default(CancellationToken))
        {
            return await MediatorHandler.Send(request, ct);
        }
    }
}