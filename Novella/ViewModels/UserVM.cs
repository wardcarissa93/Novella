using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class UserVM
    {
        [Key]
        [Required]
        public string Email { get; set; }
    }
}
