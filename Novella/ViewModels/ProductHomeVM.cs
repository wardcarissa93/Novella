using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class ProductHomeVM
    {
        [Key]
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public string Description { get; set; }
    }
}
