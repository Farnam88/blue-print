using System;

namespace TestAssignment.Domain.Common.BaseEntities
{
    public interface ICreationEntity
    {
        DateTime CreateDateTime { get; set; }
    }
}