using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxJarService.TaxJar;
using System;

namespace TaxServiceTests.UnitTests
{
    [TestClass]
    public class ApiKeyAuthenticationTest
    {
        [TestMethod]    
        public void ThrowsExceptionOnNull()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new ApiKeyAuthentication(null));
        }

        [TestMethod]
        public void SetsHttpClientHeader()
        {
            string apiKey = "xxxxxxxxxxxxxxxxxxxxx";
            var client = new System.Net.Http.HttpClient();
            
            new ApiKeyAuthentication(apiKey)
                .AddAuthenticationToClient(client);

            Assert.AreEqual(
                client.DefaultRequestHeaders.Authorization.ToString(),
                $"Bearer {apiKey}");
        }
    }
}