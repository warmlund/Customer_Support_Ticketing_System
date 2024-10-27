using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_DAL
{
    public interface ICustomerSupportDAL
    {
        List<Ticket> LoadTickets();
        bool SaveTickets(List<Ticket> tickets);
        List<Customer> LoadCustomers();
        bool SaveCustomers(List<Customer> customers);
    }
}
