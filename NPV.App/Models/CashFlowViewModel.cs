using System.ComponentModel.DataAnnotations;

namespace NPV.App.Models
{
    public class CashFlowViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Value { get; set; }
    }
}
