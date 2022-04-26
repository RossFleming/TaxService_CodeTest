using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxJarService;
using TaxJarService.TaxJar;
using TaxJarService.TaxJar.Models;

namespace TaxServiceTests.UnitTests
{
    [TestClass]
    public class TaxJarTaxCalculatorTest
    {
        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            decimal taxRate = 0.8m;
            TaxJarTaxCalculator taxCalculator =
                new TaxJarTaxCalculator(new MockTaxJarClient(taxRate));

            TaxJarOrder order = new TaxJarOrder()
            {
                OrderAmount = 123.12m,
                ShippingAmount = 21.00m,
                ToCountry = "US",
                ToZip = "10002",
                ToState = "NY"
            };

            decimal taxCollected = await taxCalculator.CalculateTaxesForOrderAsync(order);
            Assert.AreEqual(taxCollected, order.OrderAmount * taxRate);
        }

        [TestMethod]
        public async Task GetTaxRate()
        {
            decimal taxRate = 0.8m;
            TaxJarTaxCalculator taxCalculator =
                new TaxJarTaxCalculator(new MockTaxJarClient(taxRate));
           
            Assert.AreEqual(
               await taxCalculator.GetTaxRateForLocationAsync("10002"), 
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

        public Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(IOrder order, CancellationToken cancellationToken = default)
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

        public Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode, CancellationToken cancellationToken = default)
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
