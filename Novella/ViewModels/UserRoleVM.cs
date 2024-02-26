using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class UserRoleVM
    {
        [Key]
        public int? ID { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}
