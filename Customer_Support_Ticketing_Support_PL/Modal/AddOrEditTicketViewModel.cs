using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using System.Collections.ObjectModel;
using System.Windows;

namespace Customer_Support_Ticketing_System_PL.Modal
{
    /// <summary>
    /// Class for the modal window for editing and adding tickets
    /// </summary>
    internal class AddOrEditTicketViewModel : NotifyPropertyChanged
    {
        #region variables
        private bool _dialogResult;
        private bool _isAddingCustomer;
        private string _customerName;
        private string _ticketTitle;
        private string _ticketDescription;
        private CancellationTokenSource _tokenSource;
        private Ticket _currentTicket;
        private Customer _addinCustomer;
        private List<TicketPriority> _priority;
        private List<TicketStatus> _status;
        private ObservableCollection<Customer> _customers;
        private CustomerSupportBLL _customerSupportBLL;
        #endregion variables

        #region properties
        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public bool IsAddigCustomer { get { return _isAddingCustomer; } set { if (_isAddingCustomer != value) { _isAddingCustomer = value; OnPropertyChanged(nameof(IsAddigCustomer)); } } }
        public Ticket CurrentTicket { get { return _currentTicket; } set { if (_currentTicket != value) { _currentTicket = value; OnPropertyChanged(nameof(CurrentTicket)); AddTicket.RaiseCanExecuteChanged(); } } }
        public List<TicketPriority> TicketPriorities { get { return _priority; } set { if (_priority != value) { _priority = value; OnPropertyChanged(nameof(TicketPriorities)); } } }
        public List<TicketStatus> TicketStatuses { get { return _status; } set { if (_status != value) { _status = value; OnPropertyChanged(nameof(TicketStatuses)); } } }
        public ObservableCollection<Customer> Customers { get { return _customers; } set { if (_customers != value) { _customers = value; OnPropertyChanged(nameof(Customers)); } } }
        public Customer CurrentAddedCustomer { get { return _addinCustomer; } set { if (_addinCustomer != value) { _addinCustomer = value; OnPropertyChanged(nameof(CurrentAddedCustomer)); } } }
        public string CustomerName { get { return _customerName; } set { if (_customerName != value) { _customerName = value; OnPropertyChanged(nameof(CustomerName)); FinishAddCustomer.RaiseCanExecuteChanged(); } } }
        public string TicketTitle { get { return _ticketTitle; } set { if (_ticketTitle != value) { _ticketTitle = value; OnPropertyChanged(nameof(TicketTitle)); AddTicket.RaiseCanExecuteChanged(); } } }
        public string TicketDescription { get { return _ticketDescription; } set { if (_ticketDescription != value) { _ticketDescription = value; OnPropertyChanged(nameof(TicketDescription)); AddTicket.RaiseCanExecuteChanged(); } } }
        public Action Close { get; set; }
        #endregion

        #region commands
        public Command AddTicket { get; private set; }
        public Command CancelTicket { get; private set; }
        public AsyncCommand AddCustomer { get; private set; }
        public Command FinishAddCustomer { get; private set; }
        public Command CancelAddCustomer { get; private set; }
        #endregion

        /// <summary>
        /// Constructor initiating all commands and variables
        /// The BLL layer instance variable comes from the main view model as an argument
        /// The ticket argument is null if the user is adding a new ticket
        /// </summary>
        /// <param name="customerSupportBLL"></param>
        /// <param name="ticket"></param>
        public AddOrEditTicketViewModel(CustomerSupportBLL customerSupportBLL, Ticket ticket = null)
        {
            _customerSupportBLL = customerSupportBLL;
            AddTicket = new Command(AddNewOrUpdateTicket, CanAddOrUpdateTicket);
            CancelTicket = new Command(CancelAddOrUpdateTicket, CanCancelAddOrUpdateTicket);
            AddCustomer = new AsyncCommand(AddNewCustomerAsync, CanAddNewCustomer);
            FinishAddCustomer = new Command(FinishAddNewCustomer, CanFinishAddNewCustomer);
            CancelAddCustomer = new Command(CancelAddNewCustomer, CanCancelAddNewCustomer);
            TicketPriorities = TicketPriority.GetValues(typeof(TicketPriority)).Cast<TicketPriority>().ToList();
            TicketStatuses = TicketStatus.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList();
            Customers = new ObservableCollection<Customer>();
            IsAddigCustomer = false;
            CurrentAddedCustomer = new Customer();
            CustomerName = string.Empty;
            LoadCustomers();

            if (ticket != null)
            {
                CurrentTicket = ticket;
                TicketTitle = ticket.Title;
                TicketDescription = ticket.Description;
                CurrentTicket.Customer = _customerSupportBLL.CStorage.GetCustomer(ticket.Customer.Name);
            }

            else
            {
                CurrentTicket = new Ticket();
                TicketTitle = string.Empty;
                TicketDescription = string.Empty;
                CurrentTicket.Customer = Customers[0];
            }
        }

        /// <summary>
        /// Loads customers from the customer storage
        /// </summary>
        private void LoadCustomers()
        {
            Customers.Clear();
            foreach (Customer customer in _customerSupportBLL.CStorage.GetAllCustomers())
            {
                Customers.Add(customer);
            }
        }

        private bool CanCancelAddNewCustomer() => true;

        /// <summary>
        /// If the user cancels adding a customer
        /// The current customer properties are reset
        /// </summary>
        private void CancelAddNewCustomer()
        {
            IsAddigCustomer = false;
            CurrentAddedCustomer.Name = string.Empty;
            CurrentAddedCustomer.CustomerId = 0;
            CustomerName = string.Empty;
        }

        /// <summary>
        /// Checks if the user can add a new customer
        /// by checking if the customer name is empty or not
        /// </summary>
        /// <returns></returns>
        private bool CanFinishAddNewCustomer()
        {
            if (IsAddigCustomer == true)
            {
                if (CustomerName != string.Empty)
                    return true;
                else
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Finishes adding the customer
        /// If the customer name already exists, the user gets a message
        /// If not, the customer is added to the storage, and set to the current ticket
        /// </summary>
        private void FinishAddNewCustomer()
        {

            if (_customerSupportBLL.DoesCustomerExist(CustomerName))
            {
                MessageBox.Show("Customer already exists");
                IsAddigCustomer = true;
            }

            else
            {
                IsAddigCustomer = false;
                CurrentAddedCustomer.Name = CustomerName;
                _customerSupportBLL.AddNewCustomer(CurrentAddedCustomer);
                LoadCustomers();

                CurrentTicket.Customer = _customerSupportBLL.CStorage.GetCustomer(CustomerName);
                OnPropertyChanged(nameof(CurrentTicket));
                CustomerName = string.Empty;
                CurrentAddedCustomer = new Customer();
            }

        }

        private bool CanAddNewCustomer() => true;

        /// <summary>
        /// Async task when adding a new customer
        /// </summary>
        /// <returns></returns>
        private async Task AddNewCustomerAsync()
        {
            IsAddigCustomer = true;
            _tokenSource = new CancellationTokenSource();
            FinishAddCustomer.RaiseCanExecuteChanged();
            while (IsAddigCustomer)
            {
                if (_tokenSource.IsCancellationRequested)
                {
                    IsAddigCustomer = false;
                    break;
                }

                await Task.Delay(100, _tokenSource.Token);
            }

        }

        private bool CanCancelAddOrUpdateTicket() => true;

        /// <summary>
        /// If the user cancels adding a ticket, dialog result is set to false
        /// Close Action is invoked
        /// </summary>
        private void CancelAddOrUpdateTicket()
        {
            DialogResult = false;
            Close?.Invoke();
        }

        /// <summary>
        /// Checks if all data has been filled by the user
        /// if true, the user can add a ticket, if false, they haven't
        /// filled out all information yet
        /// </summary>
        /// <returns></returns>
        private bool CanAddOrUpdateTicket()
        {
            if (TicketTitle != string.Empty && TicketDescription != string.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// When the user adds a new ticket 
        /// the dialog result is set to true
        /// The current ticket is set with the title and description properties
        /// Close is invoked
        /// </summary>
        private void AddNewOrUpdateTicket()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;
            DialogResult = true;
            Close?.Invoke();
        }

    }
}
