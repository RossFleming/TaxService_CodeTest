using System;
using System.Net.Http.Headers;

namespace TaxJarService.TaxJar
{
	public class ApiKeyAuthentication : ITaxJarAuthentication
	{
		private string _apiKey;

		public ApiKeyAuthentication(string apiKey)
		{
			if(apiKey == null) throw new ArgumentNullException(nameof(apiKey));

			_apiKey = apiKey;
		}

        public void AddAuthenticationToClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }
    }
}

