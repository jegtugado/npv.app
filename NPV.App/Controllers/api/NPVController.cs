using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPV.App.Data;
using NPV.App.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NPV.App.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPVController : ControllerBase
    {
        private readonly NPVContext context;

        public NPVController(NPVContext context)
        {
            this.context = context;
        }

        [HttpGet("data")]
        public async Task<IActionResult> GetData(decimal discountRate)
        {
            var cashFlows = await this.context.CashFlows.AsNoTracking().ToListAsync();
            var data = cashFlows.Select((r, i) => new DataViewModel(i + 1, discountRate, r.Value));

            return Ok(data);
        }
    }
}
