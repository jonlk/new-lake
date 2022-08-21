public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.ToTable(nameof(TestEntity));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(150).IsRequired();

        builder.Property(x => x.CreatedBy).HasMaxLength(50).IsRequired();

        builder.Property(x => x.CreatedDate).IsRequired();

        builder.Property(x => x.ModifiedBy).HasMaxLength(50).IsRequired();

        builder.Property(x => x.ModifiedDate).IsRequired();

        builder.HasData(new TestEntity
        {
            Id = Guid.NewGuid(),
            Name = "Test Name 1",
            Description = "My test description 1",
            CreatedBy = "system",
            CreatedDate = DateTime.UtcNow,
            ModifiedBy = "system",
            ModifiedDate = DateTime.UtcNow
        },
        new TestEntity
        {
            Id = Guid.NewGuid(),
            Name = "Test Name 2",
            Description = "My test description 2",
            CreatedBy = "system",
            CreatedDate = DateTime.UtcNow,
            ModifiedBy = "system",
            ModifiedDate = DateTime.UtcNow
        });
    }
}