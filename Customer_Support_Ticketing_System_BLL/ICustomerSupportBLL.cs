using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    /// <summary>
    /// Interface for the BLL layer
    /// </summary>
    public interface ICustomerSupportBLL
    {
        bool LoadTicketsFromData();
        bool LoadCustomersFromData();
        bool SaveTicketsToData();
        bool SaveCustomersToData();
        void AddNewTicket(Ticket ticket);
        void AddNewCustomer(Customer customer);
        void RemoveTicket(Ticket ticket);
        void EditTicket(Ticket ticket, int ticketId);
    }
}
