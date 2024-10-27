using Newtonsoft.Json;

namespace Customer_Support_Ticketing_System_DTO
{
    public class Ticket
    {
        [JsonProperty("TicketID")]
        public int TicketId { get; set; }

        [JsonProperty("CustomerID")]
        public int CustomerId { get; set; }             // Foreign key reference to Customer

        [JsonProperty("Title")]
        public string Title { get; set; }               // Short title describing the issue

        [JsonProperty("Description")]
        public string Description { get; set; }         // Detailed description of the issue

        [JsonProperty("Date")]
        public DateTime CreatedDate { get; set; }       // Date when the ticket was created

        [JsonProperty("Status")]
        public TicketStatus Status { get; set; }        // Enum for the ticket's status (e.g., Open, In Progress, Closed)

        [JsonProperty("Priority")]
        public TicketPriority Priority { get; set; }    // Enum for the priority (e.g., Low, Medium, High)
    }
}
