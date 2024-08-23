using Archivium.WebApi.Models.FieldValues;
using FluentValidation;

namespace Archivium.WebApi.Validators.FieldValues;

public class FieldValueUpdateModelValidator : AbstractValidator<FieldValueUpdateModel>
{
    public FieldValueUpdateModelValidator()
    {
        RuleFor(x => x.Value)
            .NotNull()
            .NotEmpty()
            .WithMessage("Value is required.");

        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("Item ID must be greater than 0.");

        RuleFor(x => x.FieldId)
            .GreaterThan(0)
            .WithMessage("Field ID must be greater than 0.");
    }
}