using System;

namespace TestAssignment.Models.CommonEntities
{
    public abstract class BaseCreationEntity : BaseEntity, ICreationEntity
    {
        public DateTime CreateDateTime { get; set; }
    }
}