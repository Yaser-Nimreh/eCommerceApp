using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Implementations;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Implementations.Authentication;
using eCommerceApp.ClientLibrary.Services.Implementations.Cart;
using eCommerceApp.ClientLibrary.Services.Implementations.Catalog;
using eCommerceApp.ClientLibrary.Services.Interfaces.Authentication;
using eCommerceApp.ClientLibrary.Services.Interfaces.Cart;
using eCommerceApp.ClientLibrary.Services.Interfaces.Catalog;
using Microsoft.AspNetCore.Components.Authorization;
using NetcodeHub.Packages.Components.DataGrid;
using NetcodeHub.Packages.WebAssembly.Storage.Cookie;

namespace eCommerceApp.Client.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddNetcodeHubCookieStorageService();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IHttpClientHelper, HttpClientHelper>();

            services.AddScoped<IApiCallHelper, ApiCallHelper>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICartService, CartService>();

            services.AddScoped<IPaymentMethodService, PaymentMethodService>();

            services.AddVirtualizationService();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddScoped<RefreshTokenHandler>();

            services.AddHttpClient(Constants.ApiClient.Name, options =>
            {
                options.BaseAddress = new Uri("https://localhost:7258/api/");
            }).AddHttpMessageHandler<RefreshTokenHandler>();

            services.AddCascadingAuthenticationState();

            services.AddAuthorizationCore();

            return services;
        }
    }
}