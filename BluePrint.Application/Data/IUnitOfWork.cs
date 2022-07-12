namespace BluePrint.Application.Data;

public interface IUnitOfWork : IUnitOfWorkRepositories
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}