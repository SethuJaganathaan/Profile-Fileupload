using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Repositories.DTO.Users;

namespace Services.Validators
{
    public class UserValidator : AbstractValidator<UserCreateDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Email).EmailAddress()
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid format");

            RuleFor(x => x.ProfilePicture)
                .NotNull().WithMessage("Profilepicture is required");
        }
    }
}