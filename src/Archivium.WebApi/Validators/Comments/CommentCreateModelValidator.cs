using Archivium.WebApi.Models.Comments;
using FluentValidation;

namespace Archivium.WebApi.Validators.Comments;

public class CommentCreateModelValidator : AbstractValidator<CommentCreateModel>
{
    public CommentCreateModelValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty()
            .WithMessage("Content is required.");

        RuleFor(x => x.Time)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Time must be a valid date and not in the future.");

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

