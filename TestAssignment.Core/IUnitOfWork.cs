using System.Threading;
using System.Threading.Tasks;

namespace TestAssignment.Core
{
    public interface IUnitOfWork : IUnitOfWorkRepositories
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}