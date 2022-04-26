using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using TaxJarService.TaxJar.Models;

namespace TaxJarService.TaxJar
{
    public class TaxJarClient : ITaxJarClient
    {
        private static HttpClient? _client;
        private const string baseAddress = "https://api.taxjar.com/v2/";

        public TaxJarClient(ITaxJarAuthentication authentication)
        {
            if(_client == null)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(baseAddress);
                _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                authentication.AddAuthenticationToClient(_client);
            }
        }

        public async Task<TaxRatesResponse> GetTaxRatesAsync(string zipCode,
            CancellationToken cancellationToken = default)
        {
            if (_client == null) throw new NullReferenceException();
            
            return await _client.GetFromJsonAsync<TaxRatesResponse>($"rates/{zipCode}", cancellationToken)
                ?? throw new InvalidDataException("An unexpected error occurred");                
        }

        public async Task<OrderSalesTaxResponse> CalculateTaxesForOrderAsync(IOrder order, 
            CancellationToken cancellationToken = default)
        {
            if (_client == null) throw new NullReferenceException();

            var response = await _client.PostAsJsonAsync("taxes", order, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderSalesTaxResponse>()
                ?? throw new InvalidDataException("An unexpected error occurred");
        }
    }
}
