using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using Customer_Support_Ticketing_System_PL.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Customer_Support_Ticketing_System_PL
{
    public class ViewModel : NotifyPropertyChanged
    {
        #region variables
        private ObservableCollection<Ticket> _ticketList;
        private CustomerSupportBLL _customerSupportBLL;
        private Ticket _currentSelectedTicket;
        private string _searchTerm;
        #endregion

        #region commands
        //Commands for various commands called from the view
        public Command AddTicket { get; private set; }
        public Command DeleteTicket { get; private set; }
        public Command EditTicket { get; private set; }
        #endregion
        public ObservableCollection<Ticket> TicketList { get { return _ticketList; } set { if (_ticketList != value) { _ticketList = value; OnPropertyChanged(nameof(TicketList)); } } }
        public Ticket CurrentSelectedTicket { get { return _currentSelectedTicket; } set { if (_currentSelectedTicket != value) { _currentSelectedTicket = value; OnPropertyChanged(nameof(CurrentSelectedTicket)); } } }
        public string SearchTerm { get { return _searchTerm; } set { if(_searchTerm!=value) { _searchTerm = value; OnPropertyChanged(nameof(SearchTerm)); ApplyFilter(); } } }
       public ICollectionView FilteredTicketList { get;}
        public ViewModel(CustomerSupportBLL customerSupportBLL)
        {
            _customerSupportBLL = customerSupportBLL;
            TicketList = new ObservableCollection<Ticket>();
            FilteredTicketList = CollectionViewSource.GetDefaultView(TicketList);
            FilteredTicketList.Filter = FilterTickets;
            AddTicket = new Command(AddNewTicket, CanAddNewTicket);
            DeleteTicket = new Command(DeleteSelectedTicket, CanDeleteTicket);
            EditTicket = new Command(EditSelectedTicket, CanEditTicket);
        }

        public void UpdateCollection()
        {
            TicketList.Clear();
            foreach (var ticket in _customerSupportBLL.TStorage.GetAllTickets())
            {
                TicketList.Add(ticket);
            }
        }

        private bool CanEditTicket() => true;

        private void EditSelectedTicket()
        {
            var addTicketViewModel = new AddOrEditTicketViewModel(_customerSupportBLL, CurrentSelectedTicket);

            var addTicketView = new AddOrEditTicketView { DataContext = addTicketViewModel };

            var result = addTicketView.ShowDialog();

            if (result == true)
            {
                try
                {
                    _customerSupportBLL.EditTicket(addTicketViewModel.CurrentTicket, CurrentSelectedTicket.TicketId);
                    if (addTicketViewModel.CurrentAddedCustomer.Name != string.Empty)
                        _customerSupportBLL.AddNewCustomer(addTicketViewModel.CurrentAddedCustomer);
                }

                catch
                {
                    MessageBox.Show("Failed to edit ticket");
                }
            }
        }

        private bool CanDeleteTicket() => true;

        private void DeleteSelectedTicket()
        {
            try
            {
                _customerSupportBLL.RemoveTicket(CurrentSelectedTicket);
                UpdateCollection();
            }

            catch
            {
                MessageBox.Show("Failed to remove ticket");
            }

        }

        private bool CanAddNewTicket() => true;

        private void AddNewTicket()
        {
            var addTicketViewModel = new AddOrEditTicketViewModel(_customerSupportBLL);

            var addTicketView = new AddOrEditTicketView { DataContext = addTicketViewModel };

            var result = addTicketView.ShowDialog();

            if (result == true)
            {
                try
                {
                    _customerSupportBLL.AddNewTicket(addTicketViewModel.CurrentTicket);
                    if (addTicketViewModel.CurrentAddedCustomer.Name != string.Empty)
                        _customerSupportBLL.AddNewCustomer(addTicketViewModel.CurrentAddedCustomer);

                    UpdateCollection();
                }

                catch
                {
                    MessageBox.Show("Failed to add ticket");
                }
            }
        }

        public void SaveDataOnClosing()
        {
            bool saveTickets = _customerSupportBLL.SaveTicketsToData();
            bool saveCustomers = _customerSupportBLL.SaveCustomersToData();

            if (!saveTickets)
                MessageBox.Show("Failed to save ticket data");
            if (!saveCustomers)
                MessageBox.Show("Failed to save customer data");
        }

        public void LoadDataOnOpening()
        {
            bool loadTickets = _customerSupportBLL.LoadTicketsFromData();
            bool loadCustomers = _customerSupportBLL.LoadCustomersFromData();

            if (!loadTickets)
                MessageBox.Show("Failed to load ticket data");

            if (!loadCustomers)
                MessageBox.Show("Failed to load customer data");
        }
        private bool FilterTickets(object obj)
        {
            if (obj is not Ticket ticket) return false;
            return string.IsNullOrEmpty(SearchTerm)
                   || ticket.Title.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase)
                   || ticket.Customer.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase)
                   || ticket.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
        }

        private void ApplyFilter()
        {
            FilteredTicketList.Refresh();
        }


    }
}
