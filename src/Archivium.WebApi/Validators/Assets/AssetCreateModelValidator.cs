using Archivium.WebApi.Models.Assets;
using FluentValidation;

namespace Archivium.WebApi.Validators.Assets;

public class AssetCreateModelValidator : AbstractValidator<AssetCreateModel>
{
    public AssetCreateModelValidator()
    {
        RuleFor(asset => asset.FileType)
            .NotNull()
            .IsInEnum()
            .WithMessage(asset => $"{nameof(asset.FileType)} is not specified");

        RuleFor(asset => asset.File)
            .NotNull()
            .WithMessage(asset => $"{nameof(asset.FileType)} is not specified");
    }
}
