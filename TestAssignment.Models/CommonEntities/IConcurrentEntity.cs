using System;

namespace TestAssignment.Models.CommonEntities
{
    public interface IConcurrentEntity
    {
        TimeSpan RowVersion { get; set; }
    }
}