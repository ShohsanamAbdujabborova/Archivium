using Archivium.WebApi.Models.Comments;
using FluentValidation;

namespace Archivium.WebApi.Validators.Comments;

public class CommentUpdateModelValidator : AbstractValidator<CommentUpdateModel>
{
    public CommentUpdateModelValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty()
            .WithMessage("Content is required.");

        RuleFor(x => x.ItemId)
            .NotNull()
            .WithMessage("Item ID must be greater than 0.");

        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("User ID must be greater than 0.");

        RuleFor(x => x.ParentId)
            .NotNull()
            .When(x => x.ParentId.HasValue)
            .WithMessage("Parent ID must be greater than 0 if specified.");
    }
}
