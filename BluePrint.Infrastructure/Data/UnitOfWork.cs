using BluePrint.Infrastructure.Data.Repositories;
using BluePrint.Application.Data;
using BluePrint.Application.Data.Contexts;
using BluePrint.Application.Data.Repositories;
using BluePrint.Domain.Entities;

namespace BluePrint.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContext _context;

    private IAsyncRepository<TestTodoEntity> _testTodoRepository;

    public UnitOfWork(IDbContext context)
    {
        _context = context;
    }


    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }


    public IAsyncRepository<TestTodoEntity> TestTodoRepository
    {
        get
        {
            if (_testTodoRepository is null)
                return new AsyncRepository<TestTodoEntity>(_context);
            return _testTodoRepository;
        }
    }
}