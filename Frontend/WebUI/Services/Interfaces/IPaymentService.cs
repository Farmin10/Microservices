using System.Threading.Tasks;
using WebUI.Models.FakePaymentVM;

namespace WebUI.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
