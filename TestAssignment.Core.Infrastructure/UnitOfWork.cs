using System;
using System.Threading;
using System.Threading.Tasks;
using TestAssignment.Core.DAL.Repositories;
using TestAssignment.Core.DataLayer;
using TestAssignment.Core.Infrastructure.DAL;
using TestAssignment.Models;
using TestAssignment.Utilities.Extensions;

namespace TestAssignment.Core.Infrastructure
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