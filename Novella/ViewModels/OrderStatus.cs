using System.ComponentModel.DataAnnotations;

namespace Novella.ViewModels
{
    public class OrderStatusVM
    {
        [Key]
        public int OrderStatusId { get; set; }

        public string? StatusName { get; set; }
        public string StatusDescription { get; set; }

    }
}
