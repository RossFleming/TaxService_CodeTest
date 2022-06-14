using System.Text.Json.Serialization;

namespace TaxServiceCodeTest.TaxJar.Models;

public class OrderSalesTaxResponse
{
    public OrderTax Tax { get; set; } = new OrderTax();
}

public class OrderTax
{
    public decimal Rate { get; set; }

    [JsonPropertyName("taxable_amount")]
    public decimal TaxableAmount { get; set; }

    [JsonPropertyName("amount_to_collect")]
    public decimal AmountToCollect { get; set; }
}
