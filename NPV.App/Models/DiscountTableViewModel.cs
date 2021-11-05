using System.Collections.Generic;
using System.Linq;

namespace NPV.App.Models
{
    public class DiscountTableViewModel
    {
        public IEnumerable<DataViewModel> Data { get; private set; }
        public decimal NetPresentValue => this.Data.Sum(r => r.PresentValue);

        public DiscountTableViewModel(IEnumerable<DataViewModel> data)
        {
            this.Data = data;
        }
    }
}
