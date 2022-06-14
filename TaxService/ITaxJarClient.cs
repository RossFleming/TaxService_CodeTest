using TaxServiceCodeTest.TaxJar.Models;

namespace TaxServiceCodeTest.TaxJar;

public interface ITaxJarClient
{
    public Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode, CancellationToken cancellationToken);

    public Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(TaxJarOrder order, CancellationToken cancellationToken);
}
