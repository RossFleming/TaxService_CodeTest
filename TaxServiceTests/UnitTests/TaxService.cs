using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxServiceCodeTest.Core;
using TaxServiceCodeTest.TaxJar;

namespace TaxServiceCodeTest.Tests.UnitTests
{
    [TestClass]
    public class TaxServiceTest
    {
        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            decimal taxRate = 0.8m;
            decimal orderAmount = 100.0m;
            var order = new Order()
            {
                OrderAmount = orderAmount
            };

            var taxService = new TaxService(new MockTaxCalculalator(taxRate));

            Assert.AreEqual(
                await taxService.CalculateTaxesForOrderAsync(order, new CancellationToken()),
                taxRate * orderAmount);
        }

        [TestMethod]
        public async Task GetTaxRate()
        {
            decimal taxRate = 0.8m;
            var taxService = new TaxService(new MockTaxCalculalator(taxRate));

            Assert.AreEqual(
                await taxService.GetTaxRateForLocationAsync("", new CancellationToken()),
                taxRate);

        }
    }

    internal class MockTaxCalculalator : ITaxCalculator
    {
        private decimal _taxRate;
        public MockTaxCalculalator(decimal taxRate)
        {
            _taxRate = taxRate;
        }

        public Task<decimal> CalculateTaxesForOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(order.OrderAmount * _taxRate);
        }

        public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_taxRate);
        }
    }
}
