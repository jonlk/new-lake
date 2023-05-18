public record CacheItemQuery : IRequest<CacheItem>
{
    public string Key { get; init; }
}

public class CacheItemQueryHandler : IRequestHandler<CacheItemQuery, CacheItem>
{
    private readonly ICacheService<CacheItem> _cacheService;

    public CacheItemQueryHandler(ICacheService<CacheItem> cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<CacheItem> Handle(CacheItemQuery request, CancellationToken cancellationToken)
    {
        var result = await _cacheService.GetAsync(request.Key);
        return result;
    }
}