using System;
using System.Net.Http.Headers;

namespace TaxServiceCodeTest.TaxJar.Authentication;

public class ApiKeyAuthentication : ITaxJarAuthentication
{
    private readonly string _apiKey;

    public ApiKeyAuthentication(string apiKey)
    {
        _apiKey = apiKey;
    }

    public void AddAuthenticationToClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }
}

