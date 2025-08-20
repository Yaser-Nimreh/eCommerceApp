namespace eCommerceApp.ClientLibrary.Helpers.Constant
{
    public static class Constants
    {
        public static class Product
        {
            public const string GetAll = "product/all";
            public const string Get = "product/single";
            public const string Create = "product/create";
            public const string Update = "product/update";
            public const string Delete = "product/delete";
        }

        public static class Category
        {
            public const string GetAll = "category/all";
            public const string Get = "category/single";
            public const string Create = "category/create";
            public const string Update = "category/update";
            public const string Delete = "category/delete";
            public const string GetProductsByCategory = "category/products-by-category";
        }

        public static class Authentication
        {
            public const string Type = "Bearer";
            public const string CreateUser = "authentication/create-user";
            public const string LoginUser = "authentication/login-user";
            public const string RefreshToken = "authentication/refresh-token";
        }

        public static class ApiCallType
        {
            public const string Get = "get";
            public const string Post = "post";
            public const string Put = "put";
            public const string Delete = "delete";
        }

        public static class Cookie
        {
            public const string Name = "token";
            public const string Path = "/";
            public const int Days = 1;
        }

        public static class ApiClient
        {
            public const string Name = "Blazor-Client";
        }

        public static class Payment
        {
            public const string GetAll = "payment/methods";
        }

        public static class Cart
        {
            public const string Checkout = "cart/checkout";
            public const string SaveCheckout = "cart/save-checkout";
            public const string GetAchieves = "cart/get-achieves";
            public const string Name = "my-cart";
        }

        public static class Administration
        {
            public const string AdminRole = "Admin";
        }
    }
}