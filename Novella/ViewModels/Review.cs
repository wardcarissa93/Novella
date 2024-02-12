using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("UserAccount")]
        public string UserId { get; set; }

        //changed from review to ReviewDescription because it cannot be the same as namespacename
        public string ReviewDescription {  get; set; }

        public DateTime DateReviewed { get; set; }


    }
}
