using Newtonsoft.Json;

namespace Catalog.API.DTOs
{
    public class PlateDto 
    { 
        public Guid Id {  get; set; }

        [JsonProperty("Registration")]
        public string Registration { get; set; }

        [JsonProperty("PurchasePrice")]
        public decimal PurchasePrice { get; set; }

        [JsonProperty("SalePrice")]
        public decimal SalePrice { get; set; }

        [JsonProperty("Letters")]
        public string Letters { get; set; }

        [JsonProperty("Numbers")]
        public int Numbers { get; set; }
    }
}
