using System;

namespace TestAssignment.Domain.Common.BaseEntities
{
    public interface IConcurrentEntity
    {
        TimeSpan RowVersion { get; set; }
    }
}