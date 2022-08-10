using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreModels.Dtos.Delete;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreAPI.ModelsValidators
{
    public class DeleteUserDtoValidator : AbstractValidator<DeleteUserDto>
    {
        public DeleteUserDtoValidator(ClothingStoreDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            User user = default;

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
                    if (!dbContext.Users.Any(u => u.Email == value))
                    {
                        context.AddFailure("Email or Password", "Wrong Email or Password.");
                    }

                    user = dbContext.Users.FirstOrDefault(u => u.Email == value);
                });

            RuleFor(x => x.Password)
                .Custom((value, context) =>
                {
                    if (user != null)
                    {
                        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, value);

                        if (result == PasswordVerificationResult.Failed)
                        {
                            context.AddFailure("Email or Password", "Wrong Email or Password.");
                        }
                    }
                });
        }
    }
}
