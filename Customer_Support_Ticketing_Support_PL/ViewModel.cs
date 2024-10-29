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

        #region properties
        public ObservableCollection<Ticket> TicketList { get { return _ticketList; } set { if (_ticketList != value) { _ticketList = value; OnPropertyChanged(nameof(TicketList)); } } }
        public Ticket CurrentSelectedTicket { get { return _currentSelectedTicket; } set { if (_currentSelectedTicket != value) { _currentSelectedTicket = value; OnPropertyChanged(nameof(CurrentSelectedTicket)); } } }
        public string SearchTerm { get { return _searchTerm; } set { if (_searchTerm != value) { _searchTerm = value; OnPropertyChanged(nameof(SearchTerm)); ApplyFilter(); } } } //Property search term bound to the textbox in the view
        public ICollectionView FilteredTicketList { get; } //An ICollectionView for enabling filtering of the datagridview
        #endregion

        public ViewModel(CustomerSupportBLL customerSupportBLL)
        {
            _customerSupportBLL = customerSupportBLL;
            TicketList = new ObservableCollection<Ticket>();
            FilteredTicketList = CollectionViewSource.GetDefaultView(TicketList); //Sets the observablecollection as the default view
            FilteredTicketList.Filter = FilterTickets; //sets the filter for the Icollectionview
            AddTicket = new Command(AddNewTicket, CanAddNewTicket);
            DeleteTicket = new Command(DeleteSelectedTicket, CanDeleteTicket);
            EditTicket = new Command(EditSelectedTicket, CanEditTicket);
        }

        /// <summary>
        /// Updates the observablecollection with tickets from the storage in the bll layer
        /// </summary>
        public void UpdateCollection()
        {
            TicketList.Clear();
            foreach (var ticket in _customerSupportBLL.TStorage.GetAllTickets())
            {
                TicketList.Add(ticket);
            }
        }

        private bool CanEditTicket() => true;

        /// <summary>
        /// Method that edits the selected ticket
        /// creates instance of modal view and view model
        /// if dialog result is true, the ticket is edited in the BLL layer
        /// </summary>
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

        /// <summary>
        /// Calls the method in the bll layer and removes the ticket from the storage
        /// Updates the collection
        /// </summary>
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

        /// <summary>
        /// Method that adds the selected ticket
        /// creates instance of modal view and view model
        /// if dialog result is true, the ticket is add to the storage in the BLL layer 
        /// </summary>
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

        /// <summary>
        /// Method that saves tickets and customers to external data
        /// These methods are called from the dependency property in the eventhandler
        /// </summary>
        public void SaveDataOnClosing()
        {
            bool saveTickets = _customerSupportBLL.SaveTicketsToData();
            bool saveCustomers = _customerSupportBLL.SaveCustomersToData();

            if (!saveTickets)
                MessageBox.Show("Failed to save ticket data");
            if (!saveCustomers)
                MessageBox.Show("Failed to save customer data");
        }

        /// <summary>
        /// Method that loads tickets and customers to external data
        /// These methods are called from the dependency property in the eventhandler
        /// </summary>
        public void LoadDataOnOpening()
        {
            bool loadTickets = _customerSupportBLL.LoadTicketsFromData();
            bool loadCustomers = _customerSupportBLL.LoadCustomersFromData();

            if (!loadTickets)
                MessageBox.Show("Failed to load ticket data");

            if (!loadCustomers)
                MessageBox.Show("Failed to load customer data");
        }

        /// <summary>
        /// A method that filters the datagridview based on title, customer name or description
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterTickets(object obj)
        {
            if (obj is not Ticket ticket) return false;
            return string.IsNullOrEmpty(SearchTerm)
                   || ticket.Title.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase)
                   || ticket.Customer.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase)
                   || ticket.Description.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// A method that refreshes the filter every time the search term is changed
        /// </summary>
        private void ApplyFilter()
        {
            FilteredTicketList.Refresh();
        }


    }
}
