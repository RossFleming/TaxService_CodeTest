using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using TaxServiceCodeTest.TaxJar.Models;
using TaxServiceCodeTest.TaxJar.Authentication;

namespace TaxServiceCodeTest.TaxJar;


public class TaxJarClient : ITaxJarClient
{
    private static HttpClient? _client;
    private const string baseAddress = "https://api.taxjar.com/v2/";

    public TaxJarClient(ITaxJarAuthentication authentication)
    {
        if (_client == null)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            authentication.AddAuthenticationToClient(_client);
        }
    }

    /// <summary>
    /// Get tax rates from TaxJar API
    /// </summary>
    /// <param name="zipCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode, CancellationToken cancellationToken)
    {
        var response = await _client!.GetAsync($"rates/{zipCode}", cancellationToken);

        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<TaxRatesResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidDataException("An unexpected error occurred");
    }


    /// <summary>
    /// Calculate taxes for an order via the TaxJar API
    /// </summary>
    /// <param name="order"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(TaxJarOrder order, CancellationToken cancellationToken)
    {
        var response = await _client!.PostAsJsonAsync("taxes", order, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<OrderSalesTaxResponse>(cancellationToken: cancellationToken)
            ?? throw new InvalidDataException("An unexpected error occurred");
    }
}
