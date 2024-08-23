using Archivium.WebApi.Models.Fields;
using FluentValidation;

namespace Archivium.WebApi.Validators.Fields;

public class FieldUpdateModelValidator : AbstractValidator<FieldUpdateModel>
{
    public FieldUpdateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Field name is required.");

        RuleFor(x => x.CollectionId)
            .GreaterThan(0)
            .WithMessage("Collection ID must be greater than 0.");
    }
}