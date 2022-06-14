using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxServiceCodeTest.TaxJar.Authentication;
using System;

namespace TaxServiceCodeTest.Tests.UnitTests
{
    [TestClass]
    public class ApiKeyAuthenticationTest
    {
        [TestMethod]
        public void SetsHttpClientHeader()
        {
            string apiKey = "xxxxxxxxxxxxxxxxxxxxx";
            var client = new System.Net.Http.HttpClient();

            new ApiKeyAuthentication(apiKey)
                .AddAuthenticationToClient(client);

            Assert.AreEqual(
                client.DefaultRequestHeaders.Authorization?.ToString(),
                $"Bearer {apiKey}");
        }
    }
}