using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.ViewModels
{
    public class UserAccountVM
    {
        [Key]
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role {  get; set; }
        public string PhoneNumber { get; set; }
        public string PayPalAccount {  get; set; }
        
        //add foreign key later
        public int ShippingId {  get; set; }

        //add foregin key later
        public int BillingId { get; set; }
    }
}
