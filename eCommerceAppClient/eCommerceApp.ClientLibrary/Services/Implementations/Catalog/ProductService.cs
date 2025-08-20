using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Catalog.Product;
using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Catalog;
using System.Net;

namespace eCommerceApp.ClientLibrary.Services.Implementations.Catalog
{
    public class ProductService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : IProductService
    {
        public async Task<ServiceResponse> CreateAsync(CreateProduct product)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Product.Create,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = product
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<CreateProduct>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Product.Delete,
                Type = Constants.ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Product.GetAll,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<IEnumerable<GetProduct>>(result);
            else
                return [];
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Product.Get,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<GetProduct>(result);
            else
                return null!;
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Product.Update,
                Type = Constants.ApiCallType.Put,
                Client = client,
                Id = null!,
                Model = product
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<UpdateProduct>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }
    }
}