namespace TaxJarService
{
    public interface ITaxCalculator
    {
        public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken = default);
        public Task<decimal> CalculateTaxesForOrderAsync(IOrder order, CancellationToken cancellationToken = default);
    }
}
