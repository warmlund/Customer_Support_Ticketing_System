using System.ComponentModel;

namespace Customer_Support_Ticketing_System_PL.HelperClasses
{
    /// <summary>
    /// Class that helps notify the UI when a property changes in the view model
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
