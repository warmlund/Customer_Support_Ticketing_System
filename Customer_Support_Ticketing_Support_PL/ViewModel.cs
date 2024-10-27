using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using Customer_Support_Ticketing_System_PL.Modal;
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
        private Ticket _currentSelectedTicket;
        #endregion

        #region commands
        //Commands for various commands called from the view
        public Command AddTicket { get; private set; }
        public Command DeleteTicket { get; private set; }
        public Command EditTicket { get; private set; }
        #endregion
        public ObservableCollection<Ticket> TicketList { get { return _ticketList; } set { if (_ticketList != value) { _ticketList = value; OnPropertyChanged(nameof(TicketList)); } } }
        public Ticket CurrentSelectedTicket { get { return _currentSelectedTicket; } set { if (_currentSelectedTicket != value) { _currentSelectedTicket = value; OnPropertyChanged(nameof(CurrentSelectedTicket)); } } }

        public ViewModel(CustomerSupportBLL customerSupportBLL)
        {
            _customerSupportBLL = customerSupportBLL;
            TicketList = new ObservableCollection<Ticket>();

            AddTicket = new Command(AddNewTicket, CanAddNewTicket);
            DeleteTicket = new Command(DeleteSelectedTicket, CanDeleteTicket);
            EditTicket = new Command(EditSelectedTicket, CanEditTicket);
        }

        private bool CanEditTicket() => true;

        private void EditSelectedTicket()
        {
            var addTicketViewModel = new AddOrEditTicketViewModel(_customerSupportBLL, true, CurrentSelectedTicket);

            var addTicketView = new AddOrEditTicketView { DataContext = addTicketViewModel };

            var result = addTicketView.ShowDialog();

            if (result == true)
            {
                try
                {
                    _customerSupportBLL.AddNewTicket(addTicketViewModel.CurrentTicket);
                }

                catch
                {
                    MessageBox.Show("Failed to add ticket");
                }
            }
        }

        private bool CanDeleteTicket()=> true;

        private void DeleteSelectedTicket()
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewTicket()=> true;

        private void AddNewTicket()
        {
            var addTicketViewModel = new AddOrEditTicketViewModel(_customerSupportBLL,false);

           var addTicketView = new AddOrEditTicketView { DataContext = addTicketViewModel };

            var result = addTicketView.ShowDialog();

            if (result == true)
            {
                try
                {
                    _customerSupportBLL.AddNewTicket(addTicketViewModel.CurrentTicket);
                }

                catch
                {
                    MessageBox.Show("Failed to add ticket");
                }
            }
        }
    }
}
