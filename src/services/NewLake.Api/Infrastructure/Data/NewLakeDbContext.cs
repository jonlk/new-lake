public partial class NewLakeDbContext : DbContext
{
    public NewLakeDbContext(DbContextOptions<NewLakeDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<TestEntity> TestEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
    }
}