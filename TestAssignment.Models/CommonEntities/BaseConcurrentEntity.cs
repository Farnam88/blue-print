using System;

namespace TestAssignment.Models.CommonEntities
{
    public abstract class BaseConcurrentEntity : EntityBase, IConcurrentEntity, ICreationEntity
    {
        public TimeSpan RowVersion { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}