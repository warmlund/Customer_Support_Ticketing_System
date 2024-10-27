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
        void LoadTicketsFromData();
        void LoadCustomersFromData();
        void SaveTicketsToData();
        void SaveCustomersToData();
        void AddNewTicket(Ticket ticket);
        void AddNewCustomer(Customer customer);
        void RemoveTicket(int ticketId);
        void EditTicket(Ticket ticket,int ticketId);
        void UpdateCustomer(Customer customer,int customerId);
        void RemoveCustomer(int customerId);
    }
}
