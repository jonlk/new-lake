public record RemoveCacheItemCommand : IRequest<string>
{
    public string Key { get; init; }
}

public class RemoveCacheItemCommandHandler : IRequestHandler<RemoveCacheItemCommand, string>
{
    private readonly ICacheService<CacheItem> _cacheService;

    public RemoveCacheItemCommandHandler(ICacheService<CacheItem> cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<string> Handle(RemoveCacheItemCommand request, CancellationToken cancellationToken)
    {
        
        throw new NotImplementedException();
    }
}