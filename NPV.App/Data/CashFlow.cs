using System.ComponentModel.DataAnnotations;

namespace NPV.App.Data
{
    public class CashFlow
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Value { get; set; }
    }
}
