using System;

namespace TestAssignment.Domain.Common.BaseEntities
{
    public abstract class BaseCreationEntity : BaseEntity, ICreationEntity
    {
        public DateTime CreateDateTime { get; set; }
    }
}