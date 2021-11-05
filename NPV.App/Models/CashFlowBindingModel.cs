using System;
using System.ComponentModel.DataAnnotations;

namespace NPV.App.Models
{
    public class CashFlowBindingModel
    {
        public int Id { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Value { get; set; }
    }
}
