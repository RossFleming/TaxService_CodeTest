using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxJarService;
using TaxJarService.TaxJar;
using TaxJarService.TaxJar.Models;
using System;
using System.Threading.Tasks;


namespace TaxServiceTests.IntegrationTests
{
    [TestClass]
    public class TaxServiceTest
    {
        private TaxService CreateTaxService()
        {
            var keyProvider = new TaxJarKeyProvider();

            return new TaxService(
                new TaxJarTaxCalculator(
                    new TaxJarClient(
                        new ApiKeyAuthentication(keyProvider.GetAPIKey()))));
        }
        
        [TestMethod]
        public async Task GetTaxRateForValidZip()
        {
            string zipCode = "10002";
            
            var taxService = CreateTaxService();

            decimal taxRate = await taxService.GetTaxRateForLocationAsync(zipCode);
            
            Console.WriteLine(taxRate.ToString("n5"));
            Assert.IsTrue(taxRate >= 0m);
        }

        [TestMethod]
        public async Task CalculateTaxesForOrder()
        {
            TaxJarOrder order = new TaxJarOrder()
            {
                OrderAmount = 123.12m,
                ShippingAmount = 21.00m,
                ToCountry = "US",
                ToZip = "10002",
                ToState = "NY"
            };

            var taxService = CreateTaxService();

            decimal taxesToCollect = await taxService.CalculateTaxesForOrderAsync(order);

            Console.WriteLine(taxesToCollect);
            Assert.IsTrue(taxesToCollect >= 0);
        }
               
    }
}
