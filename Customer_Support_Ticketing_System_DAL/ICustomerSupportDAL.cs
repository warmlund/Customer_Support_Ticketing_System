using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_DAL
{
    /// <summary>
    /// Interface for the DAL layer
    /// </summary>
    public interface ICustomerSupportDAL
    {
        List<Ticket> LoadTickets();
        bool SaveTickets(List<Ticket> tickets);
        List<Customer> LoadCustomers();
        bool SaveCustomers(List<Customer> customers);
    }
}
