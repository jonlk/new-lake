public record AddCacheItemCommand : IRequest<string>
{
    public CacheItem CacheItem { get; init; }
}

public class AddCacheItemCommandHandler : IRequestHandler<AddCacheItemCommand, string>
{
    private readonly ICacheService<CacheItem> _cacheService;

    public AddCacheItemCommandHandler(ICacheService<CacheItem> cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<string> Handle(AddCacheItemCommand request, CancellationToken cancellationToken)
    {
        var value = await _cacheService
               .SetAsync(request.CacheItem);

        return value.Key;
    }
}