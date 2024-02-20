using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class ProductCartVM
    {
        [Key]
        [Display(Name = "Product Cart Id")]
        public int ProductCartId { get; set; }

        [ForeignKey("Product")]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Quantity")]
        public int QuantityInCart { get; set; }

        [ForeignKey("Cart")]
        [Display(Name = "Cart Id")]
        public int CartId { get; set; }

    }
}
