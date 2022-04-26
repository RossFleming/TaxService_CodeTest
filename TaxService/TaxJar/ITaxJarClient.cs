using TaxJarService.TaxJar.Models;

namespace TaxJarService.TaxJar
{
    public interface ITaxJarClient
    {
        public Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode,
            CancellationToken cancellationToken = default);

        public Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(IOrder order,
            CancellationToken cancellationToken = default);
    }
}
