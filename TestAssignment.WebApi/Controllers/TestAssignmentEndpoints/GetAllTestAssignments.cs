using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestAssignment.Services.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Services.Contexts.TestAssignmentServices.Queries;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.WebApi.Controllers.Bases;

namespace TestAssignment.WebApi.Controllers.TestAssignmentEndpoints
{
    [Route(EndpointsBaseRouteNames.TestAssignment)]
    public class GetAllTestAssignments : BaseEndpointController.WithoutRequest.WithResponse<
        ResultModel<IList<TestAssignmentDto>>>
    {
        public GetAllTestAssignments(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [SwaggerOperation(
            Tags = new[] {EndpointTagNames.TestAssignment},
            OperationId = "test-assignment.GetAll",
            Summary = "Get all test assignments",
            Description = "Get all test assignments"
        )]
        [ProducesResponseType(typeof(ResultModel<IList<TestAssignmentDto>>), 200)]
        public override async Task<ActionResult<ResultModel<IList<TestAssignmentDto>>>> HandleAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await MediatorHandler.Send(new GetTestAssignmentsQuery(), cancellationToken);
            return result;
        }
    }
}