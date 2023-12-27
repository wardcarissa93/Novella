using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("UserAccount")]
        public string UserId { get; set; }

        public DateTime DateOrdered { get; set; }

        public string PayPalTransactionId { get; set; }

        [ForeignKey("OrderStatus")]
        public int OrderStatusId { get; set; }

        // Navigation properties
        public virtual UserAccount UserAccount { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
