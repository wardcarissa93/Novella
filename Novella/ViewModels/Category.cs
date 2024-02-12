using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
