using Customer_Support_Ticketing_System_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Support_Ticketing_System_BLL
{
    public interface ICustomerSupportBLL
    {
        List<Ticket> LoadTicketsFromData();
        List<Customer> LoadCustomersFromData();
        void SaveTicketsToData(List<Ticket> tickets);
        void SaveCustomersToData(List<Customer> customers);
        void AddNewTicket(Ticket ticket);
        void AddNewCustomer(Customer customer);
        void RemoveTicket(int ticketId);
        void EditTicket(int ticketId);
        void UpdateCustomer(int customerId);
        void RemoveCustomer(int customerId);
    }
}
