using System.ComponentModel.DataAnnotations;

namespace Novella.Models
{
    public class UploadModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile ImageFile { get; set; }
    }
}
