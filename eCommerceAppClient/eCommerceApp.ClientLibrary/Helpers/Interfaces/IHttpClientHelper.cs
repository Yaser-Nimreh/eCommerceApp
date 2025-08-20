namespace eCommerceApp.ClientLibrary.Helpers.Interfaces
{
    public interface IHttpClientHelper
    {
        HttpClient GetPublicClient();
        Task<HttpClient> GetPrivateClientAsync();
    }
}