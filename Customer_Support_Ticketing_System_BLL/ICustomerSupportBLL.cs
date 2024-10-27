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
        bool LoadTicketsFromData();
        bool LoadCustomersFromData();
        bool SaveTicketsToData();
        bool SaveCustomersToData();
        void AddNewTicket(Ticket ticket);
        void AddNewCustomer(Customer customer);
        void RemoveTicket(Ticket ticket);
        void EditTicket(Ticket ticket,int ticketId);
        void UpdateCustomer(Customer customer,int customerId);
        void RemoveCustomer(Customer customer);
    }
}
