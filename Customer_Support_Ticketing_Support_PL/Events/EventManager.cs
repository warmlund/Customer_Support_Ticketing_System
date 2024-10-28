using Customer_Support_Ticketing_System_PL.Modal;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Customer_Support_Ticketing_System_PL.Events
{
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
            if (d is Window window)
            {
                window.DataContextChanged += (s, e) =>
                {
                    if (window.DataContext is AddOrEditTicketViewModel vm)
                    {
                        vm.Close = () =>
                        {
                            window.DialogResult = vm.DialogResult;
                            window.Close();
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
                window.Closing += (s, e) =>
                {
                    if (window.DataContext is ViewModel viewModel)
                    {
                        viewModel.SaveDataOnClosing();
                    }
                };

                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ViewModel viewModel)
                    {
                        viewModel.LoadDataOnOpening();
                        viewModel.UpdateCollection();
                    }

                };
            }
        }
        #endregion

        #region Attached Dependency Property for TextBox Input Change Events
        public static bool GetEnableTextInputChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableTextInputChangedProperty);
        }

        public static void SetEnableTextInputChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableTextInputChangedProperty, value);
        }

        public static readonly DependencyProperty EnableTextInputChangedProperty =
            DependencyProperty.RegisterAttached(
                "EnableTextInputChanged",   // Corrected the name to match the method name.
                typeof(bool),
                typeof(EventManager),
                new PropertyMetadata(false, OnEnableTextInputChanged));

        private static void OnEnableTextInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                // Detach any previous event handler to avoid duplicate subscriptions
                textBox.PreviewTextInput -= TextBox_PreviewTextInput;

                // If the property is set to true, attach the event handler
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += TextBox_PreviewTextInput;
                }
            }
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox && textBox.DataContext is AddOrEditTicketViewModel ticketViewModel)
            {
                ticketViewModel.AddTicket.RaiseCanExecuteChanged();
            }
        }
        #endregion
    }
}

