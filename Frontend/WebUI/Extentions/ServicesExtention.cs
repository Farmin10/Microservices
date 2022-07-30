using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebUI.Handlers;
using WebUI.Models;
using WebUI.Services;
using WebUI.Services.Interfaces;

namespace WebUI.Extentions
{
    public static class ServicesExtention
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenManager>();
            services.AddHttpClient<ICatalogService, CatalogManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IUserService, UserManager>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IBasketService, BasketManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IPaymentService, PaymentManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.FakePayment.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            services.AddHttpClient<IOrderService, OrderManager>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GetwayBaseUrl}/{serviceApiSettings.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IIdentityService, IdentityManager>();
        }
    }
}
