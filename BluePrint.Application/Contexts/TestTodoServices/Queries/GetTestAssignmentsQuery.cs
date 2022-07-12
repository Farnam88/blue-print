using BluePrint.Application.Common.Base;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Application.Contexts.TestTodoServices.Specifications;
using BluePrint.Domain.Common.Data;
using MediatR;

namespace BluePrint.Application.Contexts.TestTodoServices.Queries;

public class GetTestAssignmentsQuery : IRequest<ResultModel<IList<TestTodoDto>>>
{
}

public class
    GetTestAssignmentQueryHandler : BaseRequestHandler<GetTestAssignmentsQuery,
        ResultModel<IList<TestTodoDto>>>
{
    public GetTestAssignmentQueryHandler(Data.IUnitOfWork uow) : base(uow)
    {
    }


    public override async Task<ResultModel<IList<TestTodoDto>>> Handle(GetTestAssignmentsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new TestTodoSpecificationResult();

        var result = await Uow.TestTodoRepository.ToListAsync(specification, cancellationToken);

        return ResultModel<IList<TestTodoDto>>.Success(result: result);
    }
}