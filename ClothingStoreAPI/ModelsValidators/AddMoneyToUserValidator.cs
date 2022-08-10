using FluentValidation;

namespace ClothingStoreAPI.ModelsValidators
{
    public class AddMoneyToUserValidator : AbstractValidator<int>
    {
        public AddMoneyToUserValidator()
        {
            RuleFor(x => x)
                .GreaterThan(0)
                .WithMessage("Kwota musi być większa od 0.");
        }
    }
}
