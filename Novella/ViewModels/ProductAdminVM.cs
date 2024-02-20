using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class ProductAdminVM
    {
        [Key]
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public int QuantityInStock { get; set; }
    }
}
