namespace eCommerceApp.ClientLibrary.DTOs.Responses
{
    public record ServiceResponse(bool Success = false, string Message = null!);
}