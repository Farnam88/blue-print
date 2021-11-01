using Ardalis.Specification;
using TestAssignment.Models;
using TestAssignment.Services.Contexts.TestAssignmentServices.Dtos;

namespace TestAssignment.Services.Contexts.TestAssignmentServices.Specifications
{
    public class TestAssignmentSpecificationResult : Specification<TestAssignmentEntity, TestAssignmentDto>
    {
        public TestAssignmentSpecificationResult()
        {
            Query.Select(s => new TestAssignmentDto
            {
                Id = s.Id,
                Title = s.Title
            });
        }
    }
}