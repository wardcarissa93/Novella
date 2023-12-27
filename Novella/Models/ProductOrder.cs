using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Novella.Models
{
    public class ProductOrder
    {
        [Key]
        public int ProductOrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public int QuantityInOrder { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
