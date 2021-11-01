using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TestAssignment.Core;
using TestAssignment.Models;
using TestAssignment.Services.Common.Base;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.Services.Contexts.TestAssignmentServices.Commands
{
    public class CreateTestAssignmentCommand : IRequest<ResultModel<int>>
    {
        public string Title { get; set; }
    }

    public class CreateTestAssignmentCommandHandler : BaseRequestHandler<CreateTestAssignmentCommand, ResultModel<int>>
    {
        public CreateTestAssignmentCommandHandler(IUnitOfWork uow) : base(uow)
        {
        }

        public override async Task<ResultModel<int>> Handle(CreateTestAssignmentCommand request,
            CancellationToken cancellationToken)
        {
            Preconditions.CheckNull(request, nameof(CreateTestAssignmentCommand));

            var entity = new TestAssignmentEntity
            {
                Title = request.Title
            };
            await Uow.TestAssignmentRepository.AddAsync(entity, cancellationToken);

            await Uow.CommitAsync(cancellationToken);

            return ResultModel<int>.Success("Test assignment successfully created", entity.Id);
        }
    }
}