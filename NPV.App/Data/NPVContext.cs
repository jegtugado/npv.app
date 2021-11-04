using Microsoft.EntityFrameworkCore;

namespace NPV.App.Data
{
    public class NPVContext: DbContext
    {
        public NPVContext(DbContextOptions<NPVContext> options): base(options)
        {

        }

        public DbSet<CashFlow> CashFlows { get; set; }
    }
}
