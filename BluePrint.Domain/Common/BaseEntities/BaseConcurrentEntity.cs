namespace BluePrint.Domain.Common.BaseEntities;

public abstract class BaseConcurrentEntity : BaseEntity, IConcurrentEntity, ICreationEntity
{
    public TimeSpan RowVersion { get; set; }
    public DateTime CreateDateTime { get; set; }
}