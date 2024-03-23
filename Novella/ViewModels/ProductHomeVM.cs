using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class ProductHomeVM
    {
        [Key]
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public decimal Rating { get; set; }
        public List<string> Review { get; set; }
    }
}
