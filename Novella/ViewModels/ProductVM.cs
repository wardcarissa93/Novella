using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class ProductVM
    {
        [Key]
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }


        [Display(Name = "Stock")]
        public int QuantityAvailable { get; set; }

        [Display(Name = "Rating")]
        public decimal Rating { get; set; }
      
        public List<RatingVM>? Reviews { get; set; }



        [ForeignKey("Category Id")]
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }



        // Support for multiple new image file uploads
        [Display(Name = "Upload Images")]
        public List<IFormFile> NewImageFiles { get; set; } = new List<IFormFile>();
        public IFormFile? ImageFile { get; set; }
        // Existing image filenames for display purposes
        public List<string> ImageFilenames { get; set; } = new List<string>();

        // Identifiers of images to be deleted
        public List<int?> ImageIdsToDelete { get; set; } = new List<int?>();
    }
}
