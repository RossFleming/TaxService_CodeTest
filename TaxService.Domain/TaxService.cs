namespace TaxServiceCodeTest.Core;

public class TaxService : ITaxService
{
    private readonly ITaxCalculator _taxCalculator;

    public TaxService(ITaxCalculator taxCalculator)
    {
        _taxCalculator = taxCalculator;
    }

    public Task<decimal> CalculateTaxesForOrderAsync(Order order, CancellationToken cancellationToken)
    {
        return _taxCalculator.CalculateTaxesForOrderAsync(order, cancellationToken);
    }

    public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken)
    {
        return _taxCalculator.GetTaxRateForLocationAsync(zipCode, cancellationToken);
    }
}

