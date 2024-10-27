﻿using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using System.Reflection;

namespace Customer_Support_Ticketing_System_PL.Modal
{
    internal class AddOrEditTicketViewModel : NotifyPropertyChanged
    {
        private bool _dialogResult;
        private bool _isediting;
        private bool _isAddingCustomer;
        private string _title;
        private CancellationTokenSource _tokenSource;
        private Ticket _currentTicket;
        private Customer _customer;
        private List<TicketPriority> _priority;
        private List<TicketStatus> _status;
        private List<Customer> _customers;
        private CustomerSupportBLL _customerSupportBLL;

        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public bool IsEditing { get { return _isediting; } set { if (_isediting != value) { _isediting = value; OnPropertyChanged(nameof(IsEditing)); } } }
        public bool IsAddigCustomer { get { return _isAddingCustomer; } set { if (_isAddingCustomer != value) { _isAddingCustomer = value; OnPropertyChanged(nameof(IsAddigCustomer)); } } }
        public Ticket CurrentTicket { get { return _currentTicket; } set { if (_currentTicket != value) { _currentTicket = value; OnPropertyChanged(nameof(CurrentTicket)); } } }
        public List<TicketPriority> TicketPriorities { get { return _priority; } set { if (_priority != value) { _priority = value; OnPropertyChanged(nameof(TicketPriorities)); AddTicket.RaiseCanExecuteChanged(); } } }
        public List<TicketStatus> TicketStatuses { get { return _status; } set { if (_status != value) { _status = value; OnPropertyChanged(nameof(TicketStatuses)); AddTicket.RaiseCanExecuteChanged(); } } }
        public List<Customer> Customers { get { return _customers; } set { if (_customers != value) { _customers = value; OnPropertyChanged(nameof(Customers)); AddTicket.RaiseCanExecuteChanged(); } } }
        public Customer CurrentCustomer { get { return _customer; } set { if (_customer != value) { _customer = value; OnPropertyChanged(nameof(CurrentCustomer)); FinishAddCustomer.RaiseCanExecuteChanged(); } } }
        public string Title { get { return _title; } set { if (_title != value) { _title = value; OnPropertyChanged(nameof(Title)); AddTicket.RaiseCanExecuteChanged(); } } }
        public Action Close { get; set; }
        public Command AddTicket { get; private set; }
        public Command CancelTicket { get; }
        public AsyncCommand AddCustomer { get; private set; }
        public Command FinishAddCustomer { get; private set; }
        public Command CancelAddCustomer { get; private set; }

        public AddOrEditTicketViewModel(CustomerSupportBLL customerSupportBLL, bool isEditing, Ticket ticket = null)
        {
            _customerSupportBLL = customerSupportBLL;

            if (isEditing)
            {
                CurrentTicket = ticket;
                IsEditing = true;
            }

            else
            {
                CurrentTicket = new Ticket();
            }

            AddTicket = new Command(AddNewOrUpdateTicket, CanAddOrUpdateTicket);
            CancelTicket = new Command(CancelAddOrUpdateTicket, CanCancelAddOrUpdateTicket);
            AddCustomer = new AsyncCommand(AddNewCustomerAsync, CanAddNewCustomer);
            FinishAddCustomer = new Command(FinishAddNewCustomer, CanFinishAddNewCustomer);
            CancelAddCustomer = new Command(CancelAddNewCustomer, CanCancelAddNewCustomer);

            TicketPriorities = TicketPriority.GetValues(typeof(TicketPriority)).Cast<TicketPriority>().ToList();
            TicketStatuses = TicketStatus.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList();
            IsAddigCustomer = false;
        }

        private bool CanCancelAddNewCustomer() => true;
        private void CancelAddNewCustomer()
        {
            IsAddigCustomer = false;
            CurrentCustomer = null;
        }

        private bool CanFinishAddNewCustomer()
        {
            if (CurrentCustomer.Name != string.Empty)
                return true;
            return false;
        }

        private void FinishAddNewCustomer()
        {
            IsAddigCustomer = false;
            CurrentTicket.Customer = CurrentCustomer;
        }

        private bool CanAddNewCustomer() => true;

        private async Task AddNewCustomerAsync()
        {
            IsAddigCustomer = true;
            _tokenSource = new CancellationTokenSource();
            while (IsAddigCustomer)
            {
                if (_tokenSource.IsCancellationRequested)
                {
                    IsEditing = false;
                    break;
                }

                CurrentCustomer = new Customer();
            }
            await Task.Delay(100, _tokenSource.Token);
        }

        private bool CanCancelAddOrUpdateTicket() => true;
        private void CancelAddOrUpdateTicket()
        {
            DialogResult = false;
            Close?.Invoke();
        }

        private bool CanAddOrUpdateTicket()
        {
            PropertyInfo[] propertyInfos = _currentTicket.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return propertyInfos.All(prop => prop.GetValue(_currentTicket) != null);
        }

        private void AddNewOrUpdateTicket()
        {
            DialogResult = true;
            Close?.Invoke();
        }
    }
}