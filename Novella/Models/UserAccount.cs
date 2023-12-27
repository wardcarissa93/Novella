using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class UserAccount
    {
        [Key]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int PhoneNumber { get; set; }
        public string PaypalAccount { get; set; }
        public string Password { get; set; }

        [ForeignKey("ShippingAddress")]
        public int ShippingId { get; set; }

        [ForeignKey("BillingAddress")]
        public int BillingId { get; set; }

        // Navigation properties
        public virtual Address ShippingAddress { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
