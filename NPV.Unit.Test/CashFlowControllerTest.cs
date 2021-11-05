using Microsoft.AspNetCore.Mvc;
using NPV.App.Controllers;
using NPV.App.Data;
using NPV.App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NPV.Unit.Test
{
    public class CashFlowControllerTest : IClassFixture<NPVContextFixture>
    {
        private readonly NPVContext context;
        private readonly CashFlowController cashFlowController;

        public CashFlowControllerTest(NPVContextFixture npvContextFixture)
        {
            // Arrange
            this.context = npvContextFixture.Context;
            this.cashFlowController = new CashFlowController(this.context);
        }

        [Theory, MemberData(nameof(DummyAddCashFlow))]
        public async Task AddCashFlow_Creates_CashFlow(CashFlowBindingModel model)
        {
            // Act
            var result = await this.cashFlowController.AddCashFlow(model);
            // Assert
            var statusResult = Assert.IsType<OkResult>(result);
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

            Assert.Equal(DummyAddCashFlow.Count(), model.Count());
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

            Assert.Equal(DummyAddCashFlow.Count(), model.Data.Count());
            Assert.All(model.Data, item => item.CashFlow = 10000);
        }

        public static IEnumerable<object[]> DummyAddCashFlow =>
        new List<object[]>
        {
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            } },
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            } },
            new object[] { new CashFlowBindingModel() {
                Value = 10000
            } }
        };
    }
}
