using System;

namespace NPV.App.Models
{
    public class DataViewModel
    {
        public int Year { get; private set; }
        public decimal DiscountRate { get; private set; }
        // DF = 1 / (1 + DR)^Year
        public decimal DiscountFactor => (1 / (decimal)Math.Pow((1 + ((double)DiscountRate / 100)), Year));
        public decimal CashFlow { get; set; }
        // PV = CF * DF
        public decimal PresentValue => Math.Truncate(100 * (CashFlow * DiscountFactor)) / 100;

        //DV = CF - PV
        public decimal DiscountedValue => Math.Truncate(100 * (CashFlow - PresentValue)) / 100;

        public DataViewModel(int year, decimal discountRate, decimal cashFlow)
        {
            this.Year = year;
            this.DiscountRate = discountRate;
            this.CashFlow = cashFlow;
        }
    }
}
