using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestAssignment.Core;
using TestAssignment.Services.Common.Base;
using TestAssignment.Services.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Services.Contexts.TestAssignmentServices.Specifications;
using TestAssignment.Utilities.Common.Data;

namespace TestAssignment.Services.Contexts.TestAssignmentServices.Queries
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