using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Support_Ticketing_System_BLL
{
    internal interface ICustomerSupportBLL
    {
        void LoadTicketsFromData();
        void LoadCustomersFromData();
        void SaveTicketsToData();
        void SaveCustomersToData();
        void AddNewTicket();
        void AddNewCustomer();
        void RemoveTicket(int ticketId);
        void EditTicket(int ticketId);
        void EditCustomer(int customerId);
        void RemoveCustomer(int customerId);
    }
}
