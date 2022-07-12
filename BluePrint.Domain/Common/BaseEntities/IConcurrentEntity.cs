namespace BluePrint.Domain.Common.BaseEntities;

public interface IConcurrentEntity
{
    TimeSpan RowVersion { get; set; }
}