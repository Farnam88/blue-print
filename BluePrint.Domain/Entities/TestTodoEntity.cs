using BluePrint.Domain.Common.BaseEntities;

namespace BluePrint.Domain.Entities;

public class TestTodoEntity : BaseCreationEntity
{
    public string Title { get; set; }
}