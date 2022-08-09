using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreModels.Dtos.Create;
using FluentValidation;

namespace ClothingStoreAPI.ModelsValidators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ClothingStoreDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    if (dbContext.Users.Any(u => u.Email == value))
                    {
                        context.AddFailure("Email", "This Email is already in use.");
                    }
                });
        }
    }
}
