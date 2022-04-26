using System.Text.Json.Serialization;

namespace TaxJarService
{
    // For the sake of simplicity, I'm only including the bare minimum fields
    // reqiured to calculate tax via the TaxJar API
    public interface IOrder
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
}
