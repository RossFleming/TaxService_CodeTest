namespace TaxServiceCodeTest.Core;

// TODO: Validation
// TODO: What can and can't be null?

public class Order
{
    public string? ToCountry { get; set; }
    public string? ToZip { get; set; }
    public string? ToState { get; set; }
    public decimal OrderAmount { get; set; }
    public decimal ShippingAmount { get; set; }
}
