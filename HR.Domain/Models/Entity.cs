namespace HR.Domain.Models
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public virtual string DisplayName { get; internal set; }
    }
}