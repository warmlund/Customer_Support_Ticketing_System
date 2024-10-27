using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Support_Ticketing_System_DTO
{
    public class Ticket
    {
        public int TicketId { get; set; }               // Unique identifier for the ticket
        public int CustomerId { get; set; }             // Foreign key reference to Customer
        public string Title { get; set; }               // Short title describing the issue
        public string Description { get; set; }         // Detailed description of the issue
        public DateTime CreatedDate { get; set; }       // Date when the ticket was created
        public TicketStatus Status { get; set; }        // Enum for the ticket's status (e.g., Open, In Progress, Closed)
        public TicketPriority Priority { get; set; }    // Enum for the priority (e.g., Low, Medium, High)
        public int? AssignedAgentId { get; set; }
    }
}
