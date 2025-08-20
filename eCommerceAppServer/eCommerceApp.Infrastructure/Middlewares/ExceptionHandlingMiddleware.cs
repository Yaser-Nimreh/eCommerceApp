using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceApp.Infrastructure.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DbUpdateException ex)
            {
                var logger = httpContext.RequestServices.GetRequiredService<IApplicationLogger<ExceptionHandlingMiddleware>>();
                httpContext.Response.ContentType = "application/json";
                if (ex.InnerException is SqlException innerException)
                {
                    logger.LogError(innerException, "Sql Exception");
                    switch (innerException.Number)
                    {
                        case 2627: // Unique constraint violation
                            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                            await httpContext.Response.WriteAsync("Unique constraint violation");
                            break;
                        case 515: // Cannot insert null
                            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await httpContext.Response.WriteAsync("Cannot insert null");
                            break;
                        case 547: // Foreign key constraint violation
                            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                            await httpContext.Response.WriteAsync("Foreign key constraint violation");
                            break;
                        default:
                            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await httpContext.Response.WriteAsync("An error occurred while processing your request.");
                            break;
                    }
                }
                else
                {
                    logger.LogError(ex, "Related EFCore Exception");
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await httpContext.Response.WriteAsync("An error occurred while saving the entity changes.");
                }
            }
            catch (Exception ex)
            {
                var logger = httpContext.RequestServices.GetRequiredService<IApplicationLogger<ExceptionHandlingMiddleware>>();
                logger.LogError(ex, "Unknown Exception");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An error occurred: " + ex.Message);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}