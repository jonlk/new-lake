public class TestEntity
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }

    public virtual DateTime CreatedDate { get; set; }
    public virtual string CreatedBy { get; set; }

    public virtual DateTime ModifiedDate { get; set; }
    public virtual string ModifiedBy { get; set; }
}