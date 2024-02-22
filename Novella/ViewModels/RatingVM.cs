using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class RatingVM
    {
        [Key]
        public int RatingId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string Review { get; set; }
        [ForeignKey("UserAccount")]
        public string UserId {  get; set; }
    }
}
