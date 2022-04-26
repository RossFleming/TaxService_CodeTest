using System.Text.Json.Serialization;

namespace TaxJarService.TaxJar.Models
{
    public class TaxJarOrder : IOrder
    {
        public string? ToCountry { get; set; }

        public string? ToZip { get; set; }

        public string? ToState { get; set; }
        
        public decimal OrderAmount { get; set; }
        
        public decimal ShippingAmount { get; set; }
    }
}
