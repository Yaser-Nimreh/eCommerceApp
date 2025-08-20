using eCommerceApp.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerceApp.Application.Validations.Authentication
{
    public class LoginUserValidation : AbstractValidator<LoginUser>
    {
        public LoginUserValidation() 
        {
            RuleFor(_ => _.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(_ => _.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}