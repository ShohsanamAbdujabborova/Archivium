using Archivium.WebApi.Models.Collections;
using FluentValidation;

namespace Archivium.WebApi.Validators.Collections;

public class CollectionCreateModelValidator : AbstractValidator<CollectionCreateModel>
{
    public CollectionCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Collection name is required.");

        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID must not be null or should be greater than 0");


        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Category ID must not be null or should be greater than 0");

        RuleFor(x => x.AssetId)
            .GreaterThan(0)
            .When(x => x.AssetId.HasValue)
            .WithMessage("Asset ID should be greater than 0");
    }
}

