using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    public class CustomerSupportBLL : ICustomerSupportBLL
    {
       public List<Ticket> LoadTicketsFromData()
        {
            List<Ticket> tickets = new List<Ticket>();
            return tickets;
        }
        public List<Customer> LoadCustomersFromData()
        {
            List<Customer> list = new List<Customer>();
            return list;
        }
       public void SaveTicketsToData(List<Ticket> tickets)
        {

        }
        public void SaveCustomersToData(List<Customer> customers)
        {

        }
        public void AddNewTicket(Ticket ticket)
        {

        }
        public void AddNewCustomer(Customer customer)
        {

        }
        public void RemoveTicket(int ticketId)
        {

        }
        public void EditTicket(int ticketId)
        {

        }
        public void UpdateCustomer(int customerId)
        {

        }
        public void RemoveCustomer(int customerId)
        {

        }
    }
}
