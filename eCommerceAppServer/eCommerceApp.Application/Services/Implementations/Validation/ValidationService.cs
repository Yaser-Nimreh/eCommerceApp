using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Validation;
using FluentValidation;

namespace eCommerceApp.Application.Services.Implementations.Validation
{
    public class ValidationService : IValidationService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                string errorsToString = string.Join("; ", errors);
                return new ServiceResponse { Message = errorsToString };
            }
            return new ServiceResponse { Success = true };
        }
    }
}