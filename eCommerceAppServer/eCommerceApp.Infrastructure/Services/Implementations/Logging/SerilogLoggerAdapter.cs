using eCommerceApp.Application.Services.Interfaces.Logging;
using Microsoft.Extensions.Logging;

namespace eCommerceApp.Infrastructure.Services.Implementations.Logging
{
    public class SerilogLoggerAdapter<T>(ILogger<T> logger) : IApplicationLogger<T>
    {
        public void LogError(Exception ex, string message) =>
            logger.LogError(ex, "Error occurred: {Message}", message);

        public void LogInformation(string message) =>
            logger.LogInformation("Info: {Message}", message);

        public void LogWarning(string message) =>
            logger.LogWarning("Warning: {Message}", message);
    }
}