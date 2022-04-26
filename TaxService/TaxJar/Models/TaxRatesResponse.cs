using System.Text.Json.Serialization;

namespace TaxJarService.TaxJar.Models
{
    public class TaxRatesResponse
    {
        public TaxRate Rate { get; set; } = new TaxRate();
    }

    public class TaxRate
    {
        [JsonPropertyName("combined_rate")]
        public decimal CombinedRate { get; set; }
    }
}
