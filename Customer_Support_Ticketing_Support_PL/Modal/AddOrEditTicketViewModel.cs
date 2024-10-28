using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows;

namespace Customer_Support_Ticketing_System_PL.Modal
{
    internal class AddOrEditTicketViewModel : NotifyPropertyChanged
    {
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

        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public bool IsAddigCustomer { get { return _isAddingCustomer; } set { if (_isAddingCustomer != value) { _isAddingCustomer = value; OnPropertyChanged(nameof(IsAddigCustomer)); } } }
        public Ticket CurrentTicket { get { return _currentTicket; } set { if (_currentTicket != value) { _currentTicket = value; OnPropertyChanged(nameof(CurrentTicket)); AddTicket.RaiseCanExecuteChanged();  } } }
        public List<TicketPriority> TicketPriorities { get { return _priority; } set { if (_priority != value) { _priority = value; OnPropertyChanged(nameof(TicketPriorities));} } }
        public List<TicketStatus> TicketStatuses { get { return _status; } set { if (_status != value) { _status = value; OnPropertyChanged(nameof(TicketStatuses));} } }
        public ObservableCollection<Customer> Customers { get { return _customers; } set { if (_customers != value) { _customers = value; OnPropertyChanged(nameof(Customers)); } } }
        public Customer CurrentAddedCustomer { get { return _addinCustomer; } set { if (_addinCustomer != value) { _addinCustomer = value; OnPropertyChanged(nameof(CurrentAddedCustomer)); } } }
        public string CustomerName { get { return _customerName; } set { if (_customerName != value) { _customerName = value; OnPropertyChanged(nameof(CustomerName)); FinishAddCustomer.RaiseCanExecuteChanged(); } } }
        public string TicketTitle { get { return _ticketTitle; } set { if (_ticketTitle != value) { _ticketTitle = value; OnPropertyChanged(nameof(TicketTitle)); AddTicket.RaiseCanExecuteChanged(); } } }
        public string TicketDescription { get { return _ticketDescription; } set { if (_ticketDescription != value) { _ticketDescription = value; OnPropertyChanged(nameof(TicketDescription)); AddTicket.RaiseCanExecuteChanged(); } } }
        public Action Close { get; set; }
        public Command AddTicket { get; private set; }
        public Command CancelTicket { get; private set; }
        public AsyncCommand AddCustomer { get; private set; }
        public Command FinishAddCustomer { get; private set; }
        public Command CancelAddCustomer { get; private set; }

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
                TicketTitle=ticket.Title;
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

        private void LoadCustomers()
        {
            Customers.Clear();
            foreach (Customer customer in _customerSupportBLL.CStorage.GetAllCustomers())
            {
                Customers.Add(customer);
            }
        }

        private bool CanCancelAddNewCustomer() => true;
        private void CancelAddNewCustomer()
        {
            IsAddigCustomer = false;
            CurrentAddedCustomer.Name = string.Empty;
            CurrentAddedCustomer.CustomerId = 0;
            CustomerName = string.Empty;
        }

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
        private void CancelAddOrUpdateTicket()
        {
            DialogResult = false;
            Close?.Invoke();
        }

        private bool CanAddOrUpdateTicket()
        {
           if(TicketTitle!=string.Empty && TicketDescription!=string.Empty) 
                return true;
           return false;
        }

        private void AddNewOrUpdateTicket()
        {
            CurrentTicket.Title = TicketTitle;
            CurrentTicket.Description = TicketDescription;
            DialogResult = true;
            Close?.Invoke();
        }

    }
}
