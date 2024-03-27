using Novella.EfModels;
using Novella.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class OrderVM
    {
        public string PaypalTransactionId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public AddressVM ShippingAddress { get; set; }
        public AddressVM BillingAddress { get; set; }
    }
}
