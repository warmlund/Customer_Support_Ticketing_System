using Newtonsoft.Json;

namespace Customer_Support_Ticketing_System_DTO
{
    public class Customer
    {
        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }  // Unique identifier for the customer

        [JsonProperty("Customer")]
        public string Name { get; set; } // Customer's first name

        public Customer()
        {
            CustomerId = 0;
            Name=String.Empty;
        }
    }
}
