using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Models;

namespace TestAssignment.Core
{
    public interface IUnitOfWorkRepositories
    {
        IAsyncRepository<TestAssignmentEntity> TestAssignmentRepository { get; }
    }
}