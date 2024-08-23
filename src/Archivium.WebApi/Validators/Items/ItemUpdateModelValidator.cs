using Archivium.WebApi.Models.Items;
using FluentValidation;

namespace Archivium.WebApi.Validators.Items;

public class ItemUpdateModelValidator : AbstractValidator<ItemUpdateModel>
{
    public ItemUpdateModelValidator()
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
