using Customer_Support_Customering_System_BLL;
using Customer_Support_Ticketing_System_DAL;
using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    public class CustomerSupportBLL : ICustomerSupportBLL
    {
        private CustomerStorage _customerStorage;
        private TicketStorage _ticketStorage;
        private CustomerSupportDAL _customerSupportDAL;

        public CustomerStorage CStorage { get { return _customerStorage; } }
        public TicketStorage TStorage { get { return _ticketStorage; } }

        public CustomerSupportBLL()
        {
            _ticketStorage = new TicketStorage();
            _customerStorage = new CustomerStorage();
            _customerSupportDAL = new CustomerSupportDAL();
        }
        public void LoadTicketsFromData()
        {
            foreach (Ticket ticket in _customerSupportDAL.LoadTickets())
                _ticketStorage.Add(ticket);
        }
        public void LoadCustomersFromData()
        {
            foreach (Customer customer in _customerSupportDAL.LoadCustomers())
                _customerStorage.Add(customer);
        }
        public void SaveTicketsToData()
        {
            _customerSupportDAL.SaveTickets(_ticketStorage.GetAllTickets());
        }
        public void SaveCustomersToData()
        {
            _customerSupportDAL.SaveCustomers(_customerStorage.GetAllCustomers());
        }
        public void AddNewTicket(Ticket ticket)
        {
            TStorage.Add(ticket);
        }
        public void AddNewCustomer(Customer customer)
        {
            CStorage.Add(customer);
        }
        public void RemoveTicket(int ticketId)
        {
            TStorage.RemoveAt(ticketId);
        }
        public void EditTicket(Ticket ticket, int ticketId)
        {
            TStorage.ReplaceAt(ticket, ticketId);
        }
        public void UpdateCustomer(Customer newCustomer, int customerId)
        {
            CStorage.ReplaceAt(newCustomer, customerId);
        }
        public void RemoveCustomer(int customerId)
        {
            CStorage.RemoveAt(customerId);
        }

        public bool DoesCustomerExist(string customerName)
        {
            return CStorage.DoesCustomerNameExist(customerName);
        }
    }
}
