
namespace NewLake.Api
{
    public class CacheItemValidator : AbstractValidator<CacheItem>
    {
        public CacheItemValidator()
        {
            RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("The cache key cannot be empty");

            RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("The cache value cannot be empty");
        }
    }
}