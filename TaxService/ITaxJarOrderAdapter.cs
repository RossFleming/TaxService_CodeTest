using TaxServiceCodeTest.Core;
using TaxServiceCodeTest.TaxJar.Models;

namespace TaxServiceCodeTest.TaxJar;

public interface ITaxJarOrderAdapter
{
    public TaxJarOrder GetTaxJarOrder(Order order);
}
