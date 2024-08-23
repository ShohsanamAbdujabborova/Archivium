﻿using Archivium.Service.Helpers;
using Archivium.WebApi.Models.Accounts;
using FluentValidation;

namespace Archivium.WebApi.Validators.Accounts;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(loginModel => loginModel.Password)
            .NotNull()
            .WithMessage(loginModel => $"{nameof(loginModel.Password)} is not specified");

        RuleFor(loginModel => loginModel.Phone)
            .NotNull()
            .WithMessage(loginModel => $"{nameof(loginModel.Phone)} is not specified");

        RuleFor(loginModel => loginModel.Phone)
            .Must(ValidationHelper.IsPhoneValid);

        RuleFor(loginModel => loginModel.Password)
            .Must(ValidationHelper.IsPasswordHard);
    }
}