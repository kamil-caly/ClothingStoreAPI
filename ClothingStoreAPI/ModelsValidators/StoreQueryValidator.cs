using ClothingStoreAPI.Entities;
using ClothingStoreModels.Dtos;
using FluentValidation;

namespace ClothingStoreAPI.ModelsValidators
{
    public class StoreQueryValidator : AbstractValidator<HttpQuery>
    {
        private int[] allowedPageSizes = new[] { 1, 3, 5, 10, 15 }; 
        private string[] allowedSortByColumnNames = new[] { nameof(ClothingStore.Name),
            nameof(ClothingStore.Description), nameof(ClothingStore.CreatedDate)}; 
        public StoreQueryValidator()
        {
            RuleFor(s => s.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(s => s.PageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize must be in" +
                            $"[{string.Join(",", allowedPageSizes)}]");
                    }
                });

            RuleFor(s => s.SortBy)
                .Must(value => string.IsNullOrEmpty(value) 
                    || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by must be one with [{string.Join(",",allowedSortByColumnNames)}]");
        }
    }
}
