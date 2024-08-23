using Archivium.WebApi.Models.Tags;
using FluentValidation;

namespace Archivium.WebApi.Validators.Tags;

public class TagUpdateModelValidator : AbstractValidator<TagUpdateModel>
{
    public TagUpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Tag name is required.");
    }
}
