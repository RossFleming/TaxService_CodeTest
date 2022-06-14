using TaxServiceCodeTest.TaxJar.Models;
using TaxServiceCodeTest.Core;

namespace TaxServiceCodeTest.TaxJar;

public class TaxJarOrderAdapter : ITaxJarOrderAdapter
{
    public TaxJarOrder GetTaxJarOrder(Order order)
    {
        return new TaxJarOrder()
        {
            OrderAmount = order.OrderAmount,
            ShippingAmount = order.ShippingAmount,
            ToState = order.ToState,
            ToCountry = order.ToCountry,
            ToZip = order.ToZip
        };
    }
}
