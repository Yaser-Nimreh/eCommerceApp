using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Catalog.Category;
using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration() 
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();

            CreateMap<UpdateCategory, Category>();
            CreateMap<UpdateProduct, Product>();

            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();

            CreateMap<CreateUser, ApplicationUser>();
            CreateMap<LoginUser, ApplicationUser>();

            CreateMap<PaymentMethod, GetPaymentMethod>();

            CreateMap<CreateAchieve, Achieve>();
        }
    }
}