namespace TestAssignment.Models.CommonEntities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}