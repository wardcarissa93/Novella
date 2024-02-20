using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class OrderVM
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime DateOrdered { get; set; }

        [ForeignKey("UserAccount")]
        public string? UserId { get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }

        //add foreign key
        public int ShippingAddressId { get; set; }

        //add foreign key
        public int BillingAddressId { get; set; }

    }
}
