using Newtonsoft.Json;

namespace Customer_Support_Ticketing_System_DTO
{
    /// <summary>
    /// Class for Customer
    /// Properties set for serialization and deserialization for JSON
    /// </summary>
    public class Customer
    {
        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }

        [JsonProperty("Customer")]
        public string Name { get; set; }

        public Customer()
        {
            CustomerId = 0;
            Name = string.Empty;
        }
    }
}
