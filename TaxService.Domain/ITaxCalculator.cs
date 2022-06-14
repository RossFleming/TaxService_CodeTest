namespace TaxServiceCodeTest.Core;

public interface ITaxCalculator
{
    public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken);

    public Task<decimal> CalculateTaxesForOrderAsync(Order order, CancellationToken cancellationToken);
}
