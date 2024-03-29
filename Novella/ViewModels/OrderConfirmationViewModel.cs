namespace Novella.ViewModels
{
    using Novella.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string PaypalTransactionId { get; set; }
        public string OrderStatus { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal GetTotalPrice() => CartItems.Sum(item => item.Price * item.Quantity);
        public string PaymentMethod { get; set; }
      
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }


        public string EstimatedDelivery { get; set; }
    }

}
