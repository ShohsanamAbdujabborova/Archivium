using Archivium.WebApi.Models.Categories;
using FluentValidation;

namespace Archivium.WebApi.Validators.Categories;

public class CategoryUpdateModelValidator : AbstractValidator<CategoryUpdateModel>
{
    public CategoryUpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage(Category => $"{nameof(Category.Name)} is not specified");
    }
}