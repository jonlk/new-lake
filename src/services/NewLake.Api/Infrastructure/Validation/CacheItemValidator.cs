
namespace NewLake.Api
{
    public class AddCacheItemCommandValidator : AbstractValidator<AddCacheItemCommand>
    {
        public AddCacheItemCommandValidator()
        {
            RuleFor(x => x.CacheItem.Key)
            .NotEmpty()
            .WithMessage("The cache key cannot be empty");

            RuleFor(x => x.CacheItem.Value)
            .NotEmpty()
            .WithMessage("The cache value cannot be empty");
        }
    }
}