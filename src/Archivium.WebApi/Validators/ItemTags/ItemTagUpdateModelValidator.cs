using Archivium.WebApi.Models.ItemTags;
using FluentValidation;

namespace Archivium.WebApi.Validators.ItemTags;

public class ItemTagUpdateModelValidator : AbstractValidator<ItemTagUpdateModel>
{
    public ItemTagUpdateModelValidator()
    {
        RuleFor(x => x.ItemId)
            .GreaterThan(0)
            .WithMessage("Item ID must be greater than 0.");

        RuleFor(x => x.TagId)
            .GreaterThan(0)
            .WithMessage("Tag ID must be greater than 0.");
    }
}
