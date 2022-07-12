using BluePrint.Application.Common.Base;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Domain.Common.Data;
using BluePrint.Domain.Entities;
using BluePrint.Domain.Extensions;
using MediatR;

namespace BluePrint.Application.Contexts.TestTodoServices.Commands;

public class CreateTestTodoCommand : IRequest<ResultModel<TestTodoDto>>
{
    public string Title { get; set; }
}

public class CreateTestTodoCommandHandler : BaseRequestHandler<CreateTestTodoCommand, ResultModel<TestTodoDto>>
{
    public CreateTestTodoCommandHandler(Data.IUnitOfWork uow) : base(uow)
    {
    }

    public override async Task<ResultModel<TestTodoDto>> Handle(CreateTestTodoCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new TestTodoEntity
        {
            Title = request.Title
        };
        await Uow.TestTodoRepository.AddAsync(entity, cancellationToken);

        await Uow.CommitAsync(cancellationToken);

        return ResultModel<TestTodoDto>.Success("Test Todo successfully created",
            new TestTodoDto { Id = entity.Id, Title = entity.Title });
    }
}