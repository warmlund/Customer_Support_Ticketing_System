using Newtonsoft.Json;

namespace Customer_Support_Ticketing_System_DTO
{
    /// <summary>
    /// Class for ticket
    /// Properties set for serialization and deserialization for JSON
    /// </summary>
    public class Ticket
    {
        [JsonProperty("TicketID")]
        public int TicketId { get; set; }

        [JsonProperty("Customer")]
        public Customer Customer { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("Status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("Priority")]
        public TicketPriority Priority { get; set; }

    }
}
