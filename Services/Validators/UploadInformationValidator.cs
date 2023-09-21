using FluentValidation;
using Repositories.DTO.Common;
using Microsoft.AspNetCore.Http;

namespace Services.Validators
{
    public class UploadInformationValidator : AbstractValidator<UploadInformationDTO>
    {
        public UploadInformationValidator()
        {
            RuleFor(UFD => UFD.UserId)
                .NotEmpty().WithMessage("UserId is required")
                .NotEqual(Guid.Empty).WithMessage("UserId should not be empty");

            RuleFor(UFD => UFD.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(50).WithMessage("Description should not be empty");

            RuleFor(UFD => UFD.Files)
                .NotEmpty().WithMessage("File is required")
                .Must(files => files != null && files.Count > 0)
                .WithMessage("Upload atleast one file");
        }
    }
}