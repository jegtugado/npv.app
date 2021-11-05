using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPV.App.Controllers;
using NPV.App.Data;
using NPV.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace NPV.Unit.Test
{
    public class CashFlowControllerTest : IDisposable
    {
        private readonly NPVContext context;
        private readonly CashFlowController cashFlowController;

        public CashFlowControllerTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<NPVContext>()
                         .UseInMemoryDatabase(databaseName: "CashFlow")
                         .Options;
            this.context = new NPVContext(options);
            this.cashFlowController = new CashFlowController(this.context);

            Setup();
        }

        private void Setup()
        {
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

        [Theory, MemberData(nameof(DummyAddCashFlow))]
        public async Task AddCashFlow_Creates_CashFlow(CashFlowBindingModel model, int expectedCount)
        {
            // Act
            var result = await this.cashFlowController.AddCashFlow(model);
            // Assert
            var statusResult = Assert.IsType<OkResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, statusResult.StatusCode);
            Assert.Equal(expectedCount, this.context.CashFlows.Count());
        }

        [Fact]
        public void Index_Returns_ViewResult()
        {
            // Act
            var result = this.cashFlowController.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task GetTable_Returns_PartialViewResult()
        {
            // Act
            var result = await this.cashFlowController.GetTable();
            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CashFlowViewModel>>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task DiscountTable_Returns_PartialViewResult()
        {
            // Arange
            decimal discountRate = 10;
            // Act
            var result = await this.cashFlowController.DiscountTable(discountRate);
            // Assert
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<DiscountTableViewModel>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Data.Count());
            Assert.All(model.Data, item => item.CashFlow = 10000);
        }

        public static IEnumerable<object[]> DummyAddCashFlow =>
        new List<object[]>
        {
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            }, 4 },
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            }, 4 },
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            }, 4 }
        };
    }
}
