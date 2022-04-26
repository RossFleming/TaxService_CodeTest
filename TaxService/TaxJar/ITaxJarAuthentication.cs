using System;
namespace TaxJarService.TaxJar
{
	public interface ITaxJarAuthentication
	{
		public void AddAuthenticationToClient(HttpClient client);
	}
}

