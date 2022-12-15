public record AllTestDataQuery : IRequest<IEnumerable<TestEntity>> { }

public class AllTestDataQueryHandler : IRequestHandler<AllTestDataQuery, IEnumerable<TestEntity>>
{
    private readonly NewLakeDbContext _newLakeDbContext;

    public AllTestDataQueryHandler(NewLakeDbContext newLakeDbContext)
    {
        _newLakeDbContext = newLakeDbContext;
    }

    public async Task<IEnumerable<TestEntity>> Handle(AllTestDataQuery request, CancellationToken cancellationToken)
    {
        var query = _newLakeDbContext.TestEntities;
        var results = await query.ToListAsync();
        return results;
    }
}
