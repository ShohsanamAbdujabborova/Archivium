using Archivium.WebApi.Models.Tags;
using FluentValidation;

namespace Archivium.WebApi.Validators.Tags;

public class TagCreateModelValidator : AbstractValidator<TagCreateModel>
{
    public TagCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Tag name is required.");
    }
}
