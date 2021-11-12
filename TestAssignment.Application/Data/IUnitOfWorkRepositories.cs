using TestAssignment.Application.Data.Repositories;
using TestAssignment.Domain.Entities;

namespace TestAssignment.Application.Data
{
    public interface IUnitOfWorkRepositories
    {
        IAsyncRepository<TestAssignmentEntity> TestAssignmentRepository { get; }
    }
}