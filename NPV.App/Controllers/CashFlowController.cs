using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPV.App.Data;
using NPV.App.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.App.Controllers
{
    public class CashFlowController : Controller
    {
        private readonly NPVContext context;

        public CashFlowController(NPVContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculator()
        {
            return View();
        }

        public async Task<PartialViewResult> GetTable()
        {
            var cashFlows = await context.CashFlows.AsNoTracking().ToListAsync();
            var viewModel = cashFlows.Select(r => new CashFlowViewModel() { Id = r.Id, Value = r.Value });
            
            return PartialView("_GetTable", viewModel);
        }

        public async Task<PartialViewResult> DiscountTable(decimal discountRate)
        {
            var cashFlows = await this.context.CashFlows.AsNoTracking().ToListAsync();
            var data = cashFlows.Select((r, i) => new DataViewModel(i + 1, discountRate, r.Value));
            var viewModel = new CalculatorViewModel(data);

            return PartialView("_DiscountTable", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCashFlow([FromBody] CashFlowViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cashFlow = new CashFlow() { Value = model.Value };
                await this.context.CashFlows.AddAsync(cashFlow);
                await this.context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCashFlow([FromRoute]int id, [FromBody] CashFlowViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cashFlow = await this.context.CashFlows.FindAsync(id);

                if (cashFlow != null)
                {
                    cashFlow.Value = model.Value;
                    this.context.Entry<CashFlow>(cashFlow).State = EntityState.Modified;
                    await this.context.SaveChangesAsync();

                    return Ok();
                }

                return BadRequest();
            }
            

            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCashFlow([FromRoute] int id)
        {
            var cashFlow = await this.context.CashFlows.FindAsync(id);

            if (cashFlow != null)
            {
                this.context.CashFlows.Remove(cashFlow);
                await this.context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
    }
}
