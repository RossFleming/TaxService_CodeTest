namespace TaxServiceCodeTest.TaxJar.Authentication;

public interface ITaxJarAuthentication
{
    public void AddAuthenticationToClient(HttpClient client);
}

