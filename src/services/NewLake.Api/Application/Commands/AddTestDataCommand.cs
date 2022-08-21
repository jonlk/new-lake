public record AddTestDataCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class AddTestDataCommandHandler : IRequestHandler<AddTestDataCommand, Guid>
{
    private readonly NewLakeDbContext _newLakeDbContext;

    public AddTestDataCommandHandler(NewLakeDbContext newLakeDbContext)
    {
        _newLakeDbContext = newLakeDbContext;
    }

    public async Task<Guid> Handle(AddTestDataCommand request, CancellationToken cancellationToken)
    {
        var createdDateTime = DateTime.UtcNow;
        var user = "system";

        var entity = new TestEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedDate = createdDateTime,
            CreatedBy = user,
            ModifiedDate = createdDateTime,
            ModifiedBy = user
        };

        _newLakeDbContext.TestEntities.Add(entity);
        var result = await _newLakeDbContext.SaveChangesAsync();
        return entity.Id;
    }
}