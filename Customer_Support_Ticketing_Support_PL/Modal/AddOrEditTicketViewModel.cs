using Customer_Support_Ticketing_System_DTO;
using Customer_Support_Ticketing_System_PL.Commands;
using Customer_Support_Ticketing_System_PL.HelperClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Support_Ticketing_System_PL.Modal
{
    internal class AddOrEditTicketViewModel : NotifyPropertyChanged
    {
        private bool _dialogResult;
        private bool _isediting;
        private string _title;
        private Ticket _currentTicket;
        private Customer _customer;
        private List<TicketPriority> _priority;
        private List<TicketStatus> _status;

        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public Ticket CurrentTicket { get { return _currentTicket; } set { if (_currentTicket != value) { _currentTicket = value; OnPropertyChanged(nameof(CurrentTicket)); } } }
        public List<TicketPriority> TicketPriorities { get { return _priority; } set { if (_priority != value) { _priority = value; OnPropertyChanged(nameof(TicketPriorities)); AddTicket.RaiseCanExecuteChanged(); } } }
        public List<TicketStatus> TicketStatuses { get { return _status; } set { if (_status != value) { _status = value; OnPropertyChanged(nameof(TicketStatuses)); AddTicket.RaiseCanExecuteChanged(); } } }  
        private Customer CurrentCustomer { get { return _customer; } set { if (_customer != value) { _customer = value; OnPropertyChanged(nameof(CurrentCustomer)); } } }
        public string Title { get { return _title; } set { if (_title != value) { _title = value; OnPropertyChanged(nameof(Title)); AddTicket.RaiseCanExecuteChanged(); } } }

        public Action Close { get; set; }
        public Command AddTicket { get; private set; }
        public Command CancelTicket { get; }
        public Command AddCustomer { get; private set; }

        public AddOrEditTicketViewModel(bool isEditing, Ticket ticket=null)
        {
            if (isEditing)
            {
                CurrentTicket = ticket;
            }

            else
            {
                CurrentTicket= new Ticket();
            }

            AddTicket = new Command(AddNewOrUpdateTicket, CanAddOrUpdateTicket);
            CancelTicket = new Command(CancelAddOrUpdateTicket, CanCancelAddOrUpdateTicket);
            TicketPriorities = TicketPriority.GetValues(typeof(TicketPriority)).Cast<TicketPriority>().ToList();
            TicketStatuses = TicketStatus.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList();
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
