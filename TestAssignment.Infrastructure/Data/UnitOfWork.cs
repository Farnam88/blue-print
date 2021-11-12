using System.Threading;
using System.Threading.Tasks;
using TestAssignment.Application.Data;
using TestAssignment.Application.Data.Contexts;
using TestAssignment.Application.Data.Repositories;
using TestAssignment.Domain.Entities;
using TestAssignment.Domain.Extensions;
using TestAssignment.Infrastructure.Data.Repositories;

namespace TestAssignment.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;

        private IAsyncRepository<TestAssignmentEntity> _testAssignmentRepository;

        public UnitOfWork(IDbContext context)
        {
            Preconditions.CheckNull(context, nameof(IDbContext));
            _context = context;
        }


        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }


        public IAsyncRepository<TestAssignmentEntity> TestAssignmentRepository
        {
            get { return _testAssignmentRepository ??= new AsyncRepository<TestAssignmentEntity>(_context); }
        }
    }
}