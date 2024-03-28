using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class AddressVM
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

    }
}