namespace TaxJarService
{
	public class TaxService : ITaxService
	{
		private ITaxCalculator _taxCalculator;

		public TaxService(ITaxCalculator taxCalculator)
		{
            if (taxCalculator == null) throw new ArgumentNullException(nameof(taxCalculator));
            _taxCalculator = taxCalculator;
		}

        public Task<decimal> CalculateTaxesForOrderAsync(IOrder order, CancellationToken cancellationToken = default)
        {
            return _taxCalculator.CalculateTaxesForOrderAsync(order, cancellationToken);
        }

        public Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken = default)
        {
            return _taxCalculator.GetTaxRateForLocationAsync(zipCode);
        }
    }
}

