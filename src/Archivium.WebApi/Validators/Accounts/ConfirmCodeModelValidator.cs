using Archivium.Service.Helpers;
using Archivium.WebApi.Models.Accounts;
using FluentValidation;

namespace Archivium.WebApi.Validators.Accounts;

public class ConfirmCodeModelValidator : AbstractValidator<ConfirmCodeModel>
{
    public ConfirmCodeModelValidator()
    {
        RuleFor(cc => cc.Code)
            .NotNull()
            .WithMessage(cc => $"{nameof(cc.Code)} is not specified");

        RuleFor(cc => cc.Phone)
            .Must(ValidationHelper.IsPhoneValid);
    }
}
