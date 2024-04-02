using Novella.Models;

namespace Novella.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string Email { get; set; } // Assuming FkUserId stores email
        public string Name { get; set; } // Assuming this combines User's first and last name
        public string Role { get; set; } // User role
        public string SKU { get; set; } // PayPalTransactionId
        public string Status { get; set; }
        public string DateOrdered { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }


}
