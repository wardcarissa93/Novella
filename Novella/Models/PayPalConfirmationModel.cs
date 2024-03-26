namespace Novella.Models
{

    public class PayPalConfirmationModel
    {
        public string TransactionId { get; set; }
        public decimal TotalPrice { get; set; } // Ensure this is decimal for consistency

        // Add shipping and billing address properties
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
    }

}