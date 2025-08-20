using eCommerceApp.Application.Mapping;
using eCommerceApp.Application.Services.Implementations.Authentication;
using eCommerceApp.Application.Services.Implementations.Cart;
using eCommerceApp.Application.Services.Implementations.Catalog;
using eCommerceApp.Application.Services.Implementations.Validation;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Application.Services.Interfaces.Catalog;
using eCommerceApp.Application.Services.Interfaces.Validation;
using eCommerceApp.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfiguration));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
            services.AddValidatorsFromAssemblyContaining<LoginUserValidation>();

            services.AddScoped<IValidationService, ValidationService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IPaymentMethodService, PaymentMethodService>();

            return services;
        }
    }
}