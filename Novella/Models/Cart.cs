using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        // Navigation property
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
    }
}
