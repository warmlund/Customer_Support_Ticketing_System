using Customer_Support_Customering_System_BLL;
using Customer_Support_Ticketing_System_DAL;
using Customer_Support_Ticketing_System_DTO;

namespace Customer_Support_Ticketing_System_BLL
{
    public class CustomerSupportBLL : ICustomerSupportBLL
    {
        #region instance variables
        private CustomerStorage _customerStorage;
        private TicketStorage _ticketStorage;
        private CustomerSupportDAL _customerSupportDAL;
        #endregion

        #region properties
        public CustomerStorage CStorage { get { return _customerStorage; } }
        public TicketStorage TStorage { get { return _ticketStorage; } }
        #endregion properties

        /// <summary>
        /// Constructor
        /// Initiates variabels for ticketstorage, customer storage and the DAL layer
        /// </summary>
        public CustomerSupportBLL()
        {
            _ticketStorage = new TicketStorage();
            _customerStorage = new CustomerStorage();
            _customerSupportDAL = new CustomerSupportDAL();
        }

        /// <summary>
        /// Loading tickets from data storage
        /// Calls the method from the DAL layer
        /// </summary>
        /// <returns></returns>
        public bool LoadTicketsFromData()
        {
            var ticketList = _customerSupportDAL.LoadTickets();

            if (ticketList != null)
            {
                foreach (Ticket ticket in ticketList)
                    _ticketStorage.Add(ticket);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads customers from data storage
        /// Calls the method from the DAL layer
        /// </summary>
        /// <returns></returns>
        public bool LoadCustomersFromData()
        {
            var customerList = _customerSupportDAL.LoadCustomers();

            if (customerList != null)
            {
                foreach (Customer customer in customerList)
                    _customerStorage.Add(customer);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Saves tickets to data storage
        /// Returns as bool. If true the save is successful
        /// </summary>
        /// <returns></returns>
        public bool SaveTicketsToData()
        {
            return _customerSupportDAL.SaveTickets(_ticketStorage.GetAllTickets());
        }

        /// <summary>
        /// Saves customers to data storage
        /// Returns as bool. If true the save is successful
        /// </summary>
        /// <returns></returns>
        public bool SaveCustomersToData()
        {
            return _customerSupportDAL.SaveCustomers(_customerStorage.GetAllCustomers());
        }

        /// <summary>
        /// Adds new ticket to storage
        /// Creates an id and sets the creation date
        /// </summary>
        /// <param name="ticket"></param>
        public void AddNewTicket(Ticket ticket)
        {
            ticket.TicketId = CreateTicketId();
            ticket.CreatedDate = DateTime.Now;
            TStorage.Add(ticket);
        }

        /// <summary>
        /// Add new customer to storage
        /// Creates and id
        /// </summary>
        /// <param name="customer"></param>
        public void AddNewCustomer(Customer customer)
        {
            customer.CustomerId = CreateCustomerId();
            CStorage.Add(customer);
        }

        /// <summary>
        /// Removes a ticket from the storage
        /// </summary>
        /// <param name="ticket"></param>
        public void RemoveTicket(Ticket ticket)
        {
            TStorage.Remove(ticket);
        }

        /// <summary>
        /// Replaces and edited ticket by replacing the old ticket
        /// with the new one
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="ticketId"></param>
        public void EditTicket(Ticket ticket, int ticketId)
        {
            ticket.TicketId = ticketId;
            TStorage.ReplaceAt(ticket, ticketId);
        }

        /// <summary>
        /// Checks if a customer already exists
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public bool DoesCustomerExist(string customerName)
        {
            return CStorage.DoesCustomerNameExist(customerName);
        }

        /// <summary>
        /// Creates and id for a new customer
        /// </summary>
        /// <returns></returns>
        private int CreateCustomerId()
        {
            int id = 1;
            List<int> ids = _customerStorage.GetAllCustomers().Select(m => m.CustomerId).ToList();

            while (ids.Contains(id))
            {
                id++;  // Increment ID until we find a unique one
            }

            return id;
        }

        /// <summary>
        /// Creates and id for a new ticket
        /// </summary>
        /// <returns></returns>
        private int CreateTicketId()
        {
            int id = 101;
            List<int> ids = _ticketStorage.GetAllTickets().Select(m => m.TicketId).ToList();

            while (ids.Contains(id))
            {
                id++;  // Increment ID until we find a unique one
            }

            return id;
        }
    }
}
