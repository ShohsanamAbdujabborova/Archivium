﻿using Archivium.WebApi.Models.Collections;
using FluentValidation;

namespace Archivium.WebApi.Validators.Collections;

public class CollectionUpdateModelValidator : AbstractValidator<CollectionUpdateModel>
{
    public CollectionUpdateModelValidator()
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
            .NotNull()
            .WithMessage("User ID must not be null or should be greater than 0");

        RuleFor(x => x.CategoryId)
            .NotNull()
            .WithMessage("Category ID must not be null or should be greater than 0");

        RuleFor(x => x.AssetId)
            .NotNull()
            .When(x => x.AssetId.HasValue)
            .WithMessage("Asset ID must not be null or should be greater than 0");
    }
}
