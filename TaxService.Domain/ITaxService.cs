namespace TaxServiceCodeTest.Core;

public interface ITaxService
{
    public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken);

    public Task<decimal> CalculateTaxesForOrderAsync(Order order, CancellationToken cancellationToken);
}
