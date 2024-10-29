using Customer_Support_Ticketing_System_PL.Modal;
using System.Windows;

namespace Customer_Support_Ticketing_System_PL.Events
{
    /// <summary>
    /// Class for handling events in the views using dependency properties
    /// The properties can be used in all views and view models.
    /// This minimizes the repetition of code for eventhandling
    /// </summary>
    public class EventManager
    {
        #region event for closing modal windows
        public static bool GetEnableCloseModalEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableCloseModalEventsProperty);
        }

        public static void SetEnableCloseModalEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableCloseModalEventsProperty, value);
        }

        public static readonly DependencyProperty EnableCloseModalEventsProperty = DependencyProperty.RegisterAttached("EnableCloseModalEvents", typeof(bool), typeof(EventManager), new PropertyMetadata(false, EnableCloseModalChanged));

        private static void EnableCloseModalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window) //Checks if the objet is a window
            {
                window.DataContextChanged += (s, e) => //event handler for when datacontext has changed
                {
                    if (window.DataContext is AddOrEditTicketViewModel vm) //Checks if the data context is the view model
                    {
                        vm.Close = () => //event handler for closing the window
                        {
                            window.DialogResult = vm.DialogResult; //sets the window dialog result to the viewmodel dialog result
                            window.Close(); //close the window
                        };
                    }
                };
            }
        }
        #endregion

        #region event for opening and closing main windows and loading and saving data
        public static bool GetEnableOpenCloseWindowEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableOpenCloseWindowEventsProperty);
        }

        public static void SetEnableOpenCloseWindowEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableOpenCloseWindowEventsProperty, value);
        }

        public static readonly DependencyProperty EnableOpenCloseWindowEventsProperty = DependencyProperty.RegisterAttached("EnableOpenCloseWindowEvents", typeof(bool), typeof(EventManager), new PropertyMetadata(false, EnableOpenCloseWindowChanged));

        private static void EnableOpenCloseWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Closing += (s, e) => //event handle for when window is closing
                {
                    if (window.DataContext is ViewModel viewModel) //checks if the view model is the main window's view model
                    {
                        viewModel.SaveDataOnClosing(); //calls the method for saving data to external json file
                    }
                };

                window.Loaded += (s, e) => //event handle for when window is loading
                {
                    if (window.DataContext is ViewModel viewModel)//checks if the view model is the main window's view model
                    {
                        viewModel.LoadDataOnOpening(); //calls the method for loading data from external json file
                        viewModel.UpdateCollection(); //updates the observablecollection in the view model with the loaded data
                    }
                };
            }
        }
        #endregion
    }
}

