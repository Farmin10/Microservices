using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Models.FakePaymentVM;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class PaymentManager : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments",paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
