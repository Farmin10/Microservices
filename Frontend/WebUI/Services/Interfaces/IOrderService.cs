using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models.OrderVM;

namespace WebUI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrders();
    }
}
