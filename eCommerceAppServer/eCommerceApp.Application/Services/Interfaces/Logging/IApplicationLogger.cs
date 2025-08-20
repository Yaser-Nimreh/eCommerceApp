namespace eCommerceApp.Application.Services.Interfaces.Logging
{
    public interface IApplicationLogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
    }
}