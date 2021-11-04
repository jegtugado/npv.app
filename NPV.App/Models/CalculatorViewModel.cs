using System.Collections.Generic;
using System.Linq;

namespace NPV.App.Models
{
    public class CalculatorViewModel
    {
        public IEnumerable<DataViewModel> Data { get; private set; }
        public decimal NetPresentValue => this.Data.Sum(r => r.PresentValue);

        public CalculatorViewModel(IEnumerable<DataViewModel> data)
        {
            this.Data = data;
        }
    }
}
