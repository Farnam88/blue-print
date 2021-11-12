using Ardalis.Specification;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Domain.Entities;

namespace TestAssignment.Application.Contexts.TestAssignmentServices.Specifications
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