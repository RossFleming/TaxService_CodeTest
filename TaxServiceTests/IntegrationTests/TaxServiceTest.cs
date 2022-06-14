using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using TaxServiceCodeTest.Core;
using TaxServiceCodeTest.TaxJar;
using TaxServiceCodeTest.TaxJar.Models;
using TaxServiceCodeTest.TaxJar.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaxServiceCodeTest.Tests.IntegrationTests
{
    [TestClass]
    public class TaxServiceTest
    {
        private TaxService CreateTaxService()
        {
            var keyProvider = new TaxJarKeyProvider();
            var loggerFactory = LoggerFactory.Create(c => c.AddDebug());
            var logger = loggerFactory.CreateLogger<TaxJarTaxCalculator>();

            return new TaxService(
                new TaxJarTaxCalculator(
                    new TaxJarClient(new ApiKeyAuthentication(keyProvider.GetAPIKey())),
                    new TaxJarOrderAdapter(),
                    logger));
        }

        [TestMethod]
        public async Task GetTaxRateForValidZip()
        {
            string zipCode = "10002";

            var taxService = CreateTaxService();

            decimal taxRate = await taxService.GetTaxRateForLocationAsync(zipCode, new CancellationToken());

            Console.WriteLine(taxRate.ToString("n5"));
            Assert.IsTrue(taxRate >= 0m);
        }

        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            Order order = new Order()
            {
                OrderAmount = 123.12m,
                ShippingAmount = 21.00m,
                ToCountry = "US",
                ToZip = "10002",
                ToState = "NY"
            };

            var taxService = CreateTaxService();

            decimal taxesToCollect = await taxService.CalculateTaxesForOrderAsync(order, new CancellationToken());

            Console.WriteLine(taxesToCollect);
            Assert.IsTrue(taxesToCollect >= 0);
        }
    }
}
