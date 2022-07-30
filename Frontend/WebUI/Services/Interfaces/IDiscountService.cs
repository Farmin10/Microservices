using System.Threading.Tasks;
using WebUI.Models.DiscountVM;

namespace WebUI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
