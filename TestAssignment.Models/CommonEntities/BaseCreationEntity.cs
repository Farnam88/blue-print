using System;

namespace TestAssignment.Models.CommonEntities
{
    public abstract class BaseCreationEntity : EntityBase, ICreationEntity
    {
        public DateTime CreateDateTime { get; set; }
    }
}