namespace TaxJarService
{
    public interface ITaxService
    {
        public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken = default);
        public Task<decimal> CalculateTaxesForOrderAsync(IOrder order, CancellationToken cancellationToken = default);
    }
}
