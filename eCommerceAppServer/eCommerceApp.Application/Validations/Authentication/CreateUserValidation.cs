using eCommerceApp.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication
{
    public class CreateUserValidation : AbstractValidator<CreateUser>
    {
        public CreateUserValidation() 
        {
            RuleFor(_ => _.Fullname)
                .NotEmpty().WithMessage("Full name is required.");

            RuleFor(_ => _.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(_ => _.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[^\w]").WithMessage("Password must contain at least one special character.");

            RuleFor(_ => _.ConfirmPassword)
                .Equal(_ => _.Password).WithMessage("Passwords do not match.");
        }
    }
}