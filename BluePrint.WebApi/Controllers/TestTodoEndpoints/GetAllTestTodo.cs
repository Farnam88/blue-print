using BluePrint.WebApi.Controllers.Bases;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Application.Contexts.TestTodoServices.Queries;
using BluePrint.Domain.Common.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BluePrint.WebApi.Controllers.TestTodoEndpoints;

[Route(EndpointsBaseRouteNames.TestTodo)]
public class GetAllTestTodo : BaseEndpointController.WithoutRequest.WithResponse<
    ResultModel<IList<TestTodoDto>>>
{
    public GetAllTestTodo(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [SwaggerOperation(
        Tags = new[] {EndpointTagNames.TestAssignment},
        OperationId = "test-todo.GetAll",
        Summary = "Get all test todo",
        Description = "Get all test todo"
    )]
    [ProducesResponseType(typeof(ResultModel<IList<TestTodoDto>>), 200)]
    public override async Task<ActionResult<ResultModel<IList<TestTodoDto>>>> HandleAsync(
        CancellationToken ct = default(CancellationToken))
    {
        var result = await MediatorHandler.Send(new GetTestAssignmentsQuery(), ct);
        return result;
    }
}