using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }

        // Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}
