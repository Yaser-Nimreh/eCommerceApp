using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Catalog.Category;
using eCommerceApp.ClientLibrary.DTOs.Catalog.Product;
using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Catalog;
using System.Net;

namespace eCommerceApp.ClientLibrary.Services.Implementations.Catalog
{
    public class CategoryService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : ICategoryService
    {
        public async Task<ServiceResponse> CreateAsync(CreateCategory category)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.Create,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = category
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<CreateCategory>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.Delete,
                Type = Constants.ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.GetAll,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<IEnumerable<GetCategory>>(result);
            else
                return [];
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.Get,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<GetCategory>(result);
            else
                return null!;
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.GetProductsByCategory,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(categoryId);
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<IEnumerable<GetProduct>>(result);
            else
                return [];
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Category.Update,
                Type = Constants.ApiCallType.Put,
                Client = client,
                Id = null!,
                Model = category
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<UpdateCategory>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }
    }
}