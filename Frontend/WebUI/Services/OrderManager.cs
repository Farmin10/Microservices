using Shared.Dtos;
using Shared.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Models.FakePaymentVM;
using WebUI.Models.OrderVM;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class OrderManager : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderManager(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var payment = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expration = checkoutInfoInput.Expration,
                TotalPrice = basket.TotalPrice
            };
            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Payment not complated", IsSuccessful = false };
            }
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                },

            };

            basket.BasketItems.ForEach(item =>
            {
                var orderItem = new OrderItemCreateInput()
                { 
                    ProductId=item.CourseId,
                    Price = item.GetCurrentPrice,
                    ProductName=item.CourseName,
                    PictureUrl=""
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });


            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders",orderCreateInput);
            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Order not created", IsSuccessful = false };
            }
            var orderCreatedViewModel= await response.Content.ReadFromJsonAsync<ResponseDto<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful=true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput()
                {
                    Province = checkoutInfoInput.Province,
                    District = checkoutInfoInput.District,
                    Line = checkoutInfoInput.Line,
                    Street = checkoutInfoInput.Street,
                    ZipCode = checkoutInfoInput.ZipCode
                },

            };

            basket.BasketItems.ForEach(item =>
            {
                var orderItem = new OrderItemCreateInput()
                {
                    ProductId = item.CourseId,
                    Price = item.GetCurrentPrice,
                    ProductName = item.CourseName,
                    PictureUrl = ""
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var payment = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                CVV = checkoutInfoInput.CVV,
                Expration = checkoutInfoInput.Expration,
                TotalPrice = basket.TotalPrice,
                Order=orderCreateInput,
            };
            var responsePayment = await _paymentService.ReceivePayment(payment);
            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Payment not complated", IsSuccessful = false };
            }
            await _basketService.Delete();
            return new OrderSuspendViewModel() { IsSuccessful=true};
        }

    }
}
