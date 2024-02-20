using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class ProductCategoryVM
    {
        [Key]
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
