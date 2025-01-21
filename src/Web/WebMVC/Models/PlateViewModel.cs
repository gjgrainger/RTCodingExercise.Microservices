using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class PlateViewModel
    {
        [MaxLength(7)]
        [Required]
        public string Registration { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [MaxLength(7)]
        [Required]
        public string Letters { get; set; }

        [Required]
        public int Numbers { get; set; }
    }
}
