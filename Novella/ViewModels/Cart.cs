using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class Cart
    {

        [Key]
        [Display(Name = "Cart Id")]
        public int CartId { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
    }
}
