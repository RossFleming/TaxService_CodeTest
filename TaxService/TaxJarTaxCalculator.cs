using TaxServiceCodeTest.TaxJar.Exceptions;
using TaxServiceCodeTest.TaxJar.Models;
using TaxServiceCodeTest.Core;
using Microsoft.Extensions.Logging;

namespace TaxServiceCodeTest.TaxJar;

public class TaxJarTaxCalculator : ITaxCalculator
{
    private readonly ITaxJarClient _taxJarClient;
    private readonly ITaxJarOrderAdapter _taxJarOrderAdapter;
    private readonly ILogger<TaxJarTaxCalculator> _logger;

    public TaxJarTaxCalculator(ITaxJarClient taxJarClient, ITaxJarOrderAdapter taxJarOrderAdapter, ILogger<TaxJarTaxCalculator> logger)
    {
        _taxJarClient = taxJarClient;
        _taxJarOrderAdapter = taxJarOrderAdapter;
        _logger = logger;  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="MissingRecordExecption"></exception>
    /// <exception cref="UnauthorizedException"></exception>
    /// <exception cref="ServerTransientException"></exception>
    /// <exception cref="ServerPersistentException"></exception>
    public async Task<decimal> GetTaxRateForLocationAsync(string zipCode, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retreiving tax rate for zip code {zip}", zipCode);
            TaxRatesResponse ratesResponse = await _taxJarClient.GetTaxRatesAsync(zipCode, cancellationToken);
            return ratesResponse.Rate.CombinedRate;
        }
        catch (HttpRequestException x)
        {
            HandleHttpRequestException(x);
            throw x;    // Prevents compiler error. Exception is thrown in HandleHttpRequestException
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="MissingRecordExecption"></exception>
    /// <exception cref="UnauthorizedException"></exception>
    /// <exception cref="ServerTransientException"></exception>
    /// <exception cref="ServerPersistentException"></exception>
    public async Task<decimal> CalculateTaxesForOrderAsync(Order order, CancellationToken cancellationToken)
    {
        var taxJarOrder = _taxJarOrderAdapter.GetTaxJarOrder(order);

        try
        {
            _logger.LogInformation("Calculating taxes for order");

            OrderSalesTaxResponse taxResponse =
                await _taxJarClient.CalculateTaxesForOrderAsync(taxJarOrder, cancellationToken);

            return taxResponse.Tax.AmountToCollect;
        }
        catch(HttpRequestException x)
        {
            HandleHttpRequestException(x);
            throw x;    // Prevents compiler error. Exception is thrown in HandleHttpRequestException
        }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <exception cref="MissingRecordExecption"></exception>
    /// <exception cref="UnauthorizedException"></exception>
    /// <exception cref="ServerTransientException"></exception>
    /// <exception cref="ServerPersistentException"></exception>
    private void HandleHttpRequestException(HttpRequestException x)
    {
        _logger.LogError(x, "An error occured while calling the TaxJar API");

        if (x.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new MissingRecordExecption();
        }
        else if (x.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedException();
        }
        else if (x.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
        {
            throw new ServerTransientException();
        }
        else if(x.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
        {
            throw new ServerTransientException();
        }
        else
        {
            throw new ServerPersistentException();
        }
    }
}
