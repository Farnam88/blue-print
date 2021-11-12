namespace TestAssignment.Domain.Common.BaseEntities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}