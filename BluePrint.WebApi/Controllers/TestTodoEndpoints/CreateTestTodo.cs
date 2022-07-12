using BluePrint.WebApi.Controllers.Bases;
using BluePrint.Application.Contexts.TestTodoServices.Commands;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Domain.Common.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BluePrint.WebApi.Controllers.TestTodoEndpoints;

[Route(EndpointsBaseRouteNames.TestTodo)]
public class CreateTestTodo : BaseEndpointController.WithRequest<CreateTestTodoCommand>.WithResponse<
    ResultModel<TestTodoDto>>
{
    public CreateTestTodo(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(
        Tags = new[] { EndpointTagNames.TestAssignment },
        OperationId = "test-todo.Create",
        Summary = "Add test todo",
        Description = "Create test todo"
    )]
    [ProducesResponseType(typeof(ResultModel<TestTodoDto>), 200)]
    public override async Task<ActionResult<ResultModel<TestTodoDto>>> HandleAsync(CreateTestTodoCommand request,
        CancellationToken ct = default(CancellationToken))
    {
        return await MediatorHandler.Send(request, ct);
    }
}