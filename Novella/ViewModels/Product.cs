using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }
        public string ProductDescription { get; set; }

        public int QuantityAvailable { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }


    }
}
