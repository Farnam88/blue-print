using Ardalis.Specification;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Domain.Entities;

namespace BluePrint.Application.Contexts.TestTodoServices.Specifications;

public class TestTodoSpecificationResult : Specification<TestTodoEntity, TestTodoDto>
{
    public TestTodoSpecificationResult()
    {
        Query.Select(s => new TestTodoDto
        {
            Id = s.Id,
            Title = s.Title
        });
    }
}