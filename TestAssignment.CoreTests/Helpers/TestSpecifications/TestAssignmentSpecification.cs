using Ardalis.Specification;
using TestAssignment.Models;

namespace TestAssignment.CoreTests.Helpers.TestSpecifications
{
    public sealed class TestAssignmentSpecification : Specification<TestAssignmentEntity>
    {
        public TestAssignmentSpecification(int id)
        {
            Query.Where(w => w.Id == id).AsNoTracking();
        }
    }
}