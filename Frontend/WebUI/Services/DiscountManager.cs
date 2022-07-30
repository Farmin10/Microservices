using Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Models.DiscountVM;
using WebUI.Services.Interfaces;

namespace WebUI.Services
{
    public class DiscountManager : IDiscountService
    {
        private readonly HttpClient _client;

        public DiscountManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            var response=await _client.GetAsync($"discounts/GetByCode/{discountCode}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var discount=await response.Content.ReadFromJsonAsync<ResponseDto<DiscountViewModel>>();
            return discount.Data;
        }
    }
}
