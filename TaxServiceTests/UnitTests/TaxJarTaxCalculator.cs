using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TaxServiceCodeTest.TaxJar;
using TaxServiceCodeTest.TaxJar.Models;
using TaxServiceCodeTest.Core;

namespace TaxServiceCodeTest.Tests.UnitTests
{
    [TestClass]
    public class TaxJarTaxCalculatorTest
    {
        private TaxJarTaxCalculator CreateTaxJarCalculator(decimal taxRate)
        {
            var loggerFactory = LoggerFactory.Create(c => c.AddDebug());
            var logger = loggerFactory.CreateLogger<TaxJarTaxCalculator>();

            return new TaxJarTaxCalculator(
                    new MockTaxJarClient(taxRate),
                    new TaxJarOrderAdapter(),
                    logger);
        }
        
        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            decimal taxRate = 0.8m;
            var taxCalculator = CreateTaxJarCalculator(taxRate);

            Order order = new Order()
            {
                OrderAmount = 123.12m,
                ShippingAmount = 21.00m,
                ToCountry = "US",
                ToZip = "10002",
                ToState = "NY"
            };

            decimal taxCollected = await taxCalculator.CalculateTaxesForOrderAsync(order, new CancellationToken());
            Assert.AreEqual(taxCollected, order.OrderAmount * taxRate);
        }

        [TestMethod]
        public async Task GetTaxRate()
        {
            decimal taxRate = 0.8m;
            var taxCalculator = CreateTaxJarCalculator(taxRate);

            Assert.AreEqual(
               await taxCalculator.GetTaxRateForLocationAsync("10002", new CancellationToken()),
               taxRate);
        }
    }

    internal class MockTaxJarClient : ITaxJarClient
    {
        private decimal _taxRate;
        public MockTaxJarClient(decimal taxRate)
        {
            _taxRate = taxRate;
        }

        public Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(TaxJarOrder order, CancellationToken cancellationToken)
        {
            return Task.FromResult(new OrderSalesTaxResponse()
            {
                Tax = new OrderTax()
                {
                    AmountToCollect = _taxRate * order.OrderAmount,
                    TaxableAmount = order.OrderAmount,
                    Rate = _taxRate
                }
            });
        }

        public Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TaxRatesResponse()
            {
                Rate = new TaxRate()
                {
                    CombinedRate = _taxRate
                }
            });
        }
    }
}
