using Archivium.WebApi.Models.Items;
using FluentValidation;

namespace Archivium.WebApi.Validators.Items;

public class ItemCreateModelValidator : AbstractValidator<ItemCreateModel>
{
    public ItemCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Item name is required.");

        RuleFor(x => x.CollectionId)
            .GreaterThan(0)
            .WithMessage("Collection ID must be greater than 0.");
    }
}
