using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.OrderVM
{
    public class CheckoutInfoInput
    {
        public string  Province { get; set; }
        public string  District { get; set; }
        public string Street { get; set; }
        [Display(Name = "Zip code")]
        public string  ZipCode { get; set; }
        [Display(Name = "Address")]
        public string  Line { get; set; }
        [Display(Name = "Card name")]
        public string CardName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        public string Expration { get; set; }
        public string CVV { get; set; }
    }
}
