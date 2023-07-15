public class AddTestDataCommandValidator : AbstractValidator<AddTestDataCommand>
{
    public AddTestDataCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage($"{nameof(TestEntity.Name)} is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(150)
            .WithMessage($"{nameof(TestEntity.Description)} is required");
    }
}