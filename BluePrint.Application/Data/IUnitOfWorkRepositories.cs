using BluePrint.Application.Data.Repositories;
using BluePrint.Domain.Entities;

namespace BluePrint.Application.Data;

public interface IUnitOfWorkRepositories
{
    IAsyncRepository<TestTodoEntity> TestTodoRepository { get; }
}