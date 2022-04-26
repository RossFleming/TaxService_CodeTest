using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxJarService;

namespace TaxServiceTests.UnitTests
{
    [TestClass]
    public class TaxServiceTest
    {
        [TestMethod]
        public void ThrowsExeceptionOnNullParams()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new TaxService(null));
        }

        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            decimal taxRate = 0.8m;
            decimal orderAmount = 100.0m;
            var order = new TaxJarService.TaxJar.Models.TaxJarOrder() 
            { 
                OrderAmount = orderAmount
            };

            var taxService = new TaxService(new MockTaxCalculalator(taxRate));

            Assert.AreEqual(
                await taxService.CalculateTaxesForOrderAsync(order),
                taxRate * orderAmount);
        }

        [TestMethod]
        public async Task GetTaxRate()
        {
            decimal taxRate = 0.8m;
            var taxService = new TaxService(new MockTaxCalculalator(taxRate));

            Assert.AreEqual(
                await taxService.GetTaxRateForLocationAsync(""),
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
        
        public Task<decimal> CalculateTaxesForOrderAsync(IOrder order, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(order.OrderAmount * _taxRate);
        }

        public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_taxRate);
        }
    }
}
