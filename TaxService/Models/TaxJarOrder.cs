using System.Text.Json.Serialization;

namespace TaxServiceCodeTest.TaxJar.Models;

public class TaxJarOrder
{
    [JsonPropertyName("to_country")]
    public string? ToCountry { get; set; }

    [JsonPropertyName("to_zip")]
    public string? ToZip { get; set; }

    [JsonPropertyName("to_state")]
    public string? ToState { get; set; }

    [JsonPropertyName("amount")]
    public decimal OrderAmount { get; set; }

    [JsonPropertyName("shipping")]
    public decimal ShippingAmount { get; set; }
}
