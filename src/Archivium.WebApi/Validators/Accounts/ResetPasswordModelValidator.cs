using Archivium.Service.Helpers;
using Archivium.WebApi.Models.Accounts;
using FluentValidation;

namespace Archivium.WebApi.Validators.Accounts;

public class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator()
    {
        RuleFor(rp => rp.NewPassword)
            .NotNull()
            .WithMessage(rp => $"{nameof(rp.NewPassword)} is not specified");

        RuleFor(rp => rp.Phone)
            .NotNull()
            .WithMessage(rp => $"{nameof(rp.Phone)} is not specified");

        RuleFor(rp => rp.Phone)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(rp => rp.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);
    }
}
