using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxJarService.TaxJar.Models;

namespace TaxJarService.TaxJar
{
    public class TaxJarTaxCalculator : ITaxCalculator
    {
        private ITaxJarClient _taxJarClient;

        public TaxJarTaxCalculator(ITaxJarClient taxJarClient)
        {
            _taxJarClient = taxJarClient;
        }

        public async Task<decimal> GetTaxRateForLocationAsync(string zipCode,
            CancellationToken cancellationToken = default)
        {
            TaxRatesResponse ratesResponse = await _taxJarClient.GetTaxRatesAsync(zipCode, cancellationToken);

            return ratesResponse.Rate.CombinedRate;
        }

        public async Task<decimal> CalculateTaxesForOrderAsync(IOrder order,
            CancellationToken cancellationToken = default)
        {
            OrderSalesTaxResponse taxResponse = 
                await _taxJarClient.CalculateTaxesForOrderAsync(order, cancellationToken);

            return taxResponse.Tax.AmountToCollect;
        }
    }
}
