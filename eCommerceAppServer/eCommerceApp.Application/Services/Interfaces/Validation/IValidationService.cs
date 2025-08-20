using eCommerceApp.Application.DTOs.Responses;
using FluentValidation;

namespace eCommerceApp.Application.Services.Interfaces.Validation
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator);
    }
}