using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Length { get; set; }
        public decimal Price { get; set; }
        public string MetalType { get; set; }
        public string GemStoneColor { get; set; }
        public int QuantityAvailable { get; set; }
        public string ImageUrl { get; set; }
        public int QuantityInStock { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
