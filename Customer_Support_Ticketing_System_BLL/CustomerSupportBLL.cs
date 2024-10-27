﻿using Customer_Support_Customering_System_BLL;
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
        public bool SaveTicketsToData()
        {
           return _customerSupportDAL.SaveTickets(_ticketStorage.GetAllTickets());
        }
        public bool SaveCustomersToData()
        {
            return _customerSupportDAL.SaveCustomers(_customerStorage.GetAllCustomers());
        }
        public void AddNewTicket(Ticket ticket)
        {
            TStorage.Add(ticket);
        }
        public void AddNewCustomer(Customer customer)
        {
            CStorage.Add(customer);
        }
        public void RemoveTicket(Ticket ticket)
        {
            TStorage.Remove(ticket);
        }
        public void EditTicket(Ticket ticket, int ticketId)
        {
            TStorage.ReplaceAt(ticket, ticketId);
        }
        public void UpdateCustomer(Customer newCustomer, int customerId)
        {
            CStorage.ReplaceAt(newCustomer, customerId);
        }
        public void RemoveCustomer(Customer customer)
        {
            CStorage.Remove(customer);
        }

        public bool DoesCustomerExist(string customerName)
        {
            return CStorage.DoesCustomerNameExist(customerName);
        }
    }
}
