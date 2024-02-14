using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class CategoryVM
    {
        [Key]
        [Display(Name = "Cateogory Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
    }
}
