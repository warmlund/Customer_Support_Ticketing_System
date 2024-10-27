using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer_Support_Ticketing_System_BLL;
using System.Windows;

namespace Customer_Support_Ticketing_System_PL
{
    public class ViewModel : NotifyPropertyChanged
    {
        #region variables
        private ObservableCollection<Ticket> _ticketList;
        private CustomerSupportBLL _customerSupportBLL;
        #endregion

        #region commands
        //Commands for various commands called from the view
        public Command AddNewTicket { get; private set; }
        public Command DeleteTicket { get; private set; }
        public Command EditTicket { get; private set; }
        #endregion

        public ObservableCollection<Ticket> TicketList { get { return _ticketList; } set { if (_ticketList != value) { _ticketList = value; OnPropertyChanged(nameof(TicketList)); } } }

        public ViewModel(CustomerSupportBLL customerSupportBLL)
        {
            _customerSupportBLL = customerSupportBLL;
            TicketList = new ObservableCollection<Ticket>();
            
            var ticket=new Ticket
            {
                CreatedDate = DateTime.Now,
                CustomerId = 1,
                TicketId = 1,
                Title = "Test",
                Description = "Test",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Low,
            };
            _ticketList.Add(ticket);

            var ticket2 = new Ticket
            {
                CreatedDate = DateTime.Now,
                CustomerId = 1,
                TicketId = 1,
                Title = "Test",
                Description = "Test",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Low,
            };
            _ticketList.Add(ticket2);

            OnPropertyChanged(nameof(TicketList));
        }
    }
}
