using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class ProductOrder
    {
        [Key]
        public int ProductOrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        public int QuantityInOrder { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }

    }
}
