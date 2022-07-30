using WebUI.Models.OrderVM;

namespace WebUI.Models.FakePaymentVM
{
    public class PaymentInfoInput
    {
        public string  CardName { get; set; }
        public string  CardNumber { get; set; }
        public string  Expration { get; set; }
        public string  CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderCreateInput Order { get; set; }
    }
}
