using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPV.App.Controllers.api;
using NPV.App.Data;
using NPV.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NPV.Unit.Test
{
    public class NPVControllerTest : IDisposable
    {
        private readonly NPVContext context;
        private readonly NPVController npvController;

        public NPVControllerTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NPVContext>()
                          .UseInMemoryDatabase(databaseName: "NPV")
                          .Options;
            this.context = new NPVContext(options);
            this.npvController = new NPVController(this.context);

            Setup();
        }

        private void Setup()
        {
            int count = this.context.CashFlows.Count();
            this.context.CashFlows.AddRange(
                new CashFlow() { Id = 1, Value = 10000 },
                new CashFlow() { Id = 2, Value = 10000 },
                new CashFlow() { Id = 3, Value = 10000 });
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Database.EnsureDeleted();
            this.context.Dispose();
        }

        [Fact]
        public async Task GetData_Returns_OK()
        {
            // Arange
            decimal discountRate = 10;
            // Act
            var result = await this.npvController.GetData(discountRate);
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var data = (IEnumerable<DataViewModel>)objectResult.Value;

            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode.Value);
            Assert.Equal(3, data.Count());
        }
    }
}
