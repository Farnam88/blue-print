using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestAssignment.Application.Common.Base;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Application.Contexts.TestAssignmentServices.Specifications;
using TestAssignment.Domain.Common.Data;
using IUnitOfWork = TestAssignment.Application.Data.IUnitOfWork;

namespace TestAssignment.Application.Contexts.TestAssignmentServices.Queries
{
    public class GetTestAssignmentsQuery : IRequest<ResultModel<IList<TestAssignmentDto>>>
    {
    }

    public class
        GetTestAssignmentQueryHandler : BaseRequestHandler<GetTestAssignmentsQuery,
            ResultModel<IList<TestAssignmentDto>>>
    {
        public GetTestAssignmentQueryHandler(IUnitOfWork uow) : base(uow)
        {
        }


        public override async Task<ResultModel<IList<TestAssignmentDto>>> Handle(GetTestAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            var specification = new TestAssignmentSpecificationResult();

            var result = await Uow.TestAssignmentRepository.ToListAsync(specification, cancellationToken);

            return ResultModel<IList<TestAssignmentDto>>.Success(result: result);
        }
    }
}