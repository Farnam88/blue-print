using System.Threading;
using System.Threading.Tasks;

namespace TestAssignment.Application.Data
{
    public interface IUnitOfWork : IUnitOfWorkRepositories
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}