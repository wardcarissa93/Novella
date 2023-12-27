using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Novella.Models
{
    public class ProductCart
    {
        [Key]
        public int ProductCartId { get; set; }

        [ForeignKey("Product")]
        public string ProductId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public int QuantityInCart { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
