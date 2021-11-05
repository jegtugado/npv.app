using Microsoft.EntityFrameworkCore;
using NPV.App.Data;
using System;

namespace NPV.Unit.Test
{
    public class NPVContextFixture : IDisposable
    {
        public readonly NPVContext Context;

        public NPVContextFixture()
        {
            var options = new DbContextOptionsBuilder<NPVContext>()
                          .UseInMemoryDatabase(databaseName: "NPV")
                          .Options;
            this.Context = new NPVContext(options);
        }
        public void Dispose()
        {
            this.Context.Database.EnsureDeleted();
            this.Context.Dispose();
        }
    }
}
