using Customer_Support_Ticketing_System_PL.Modal;
using System.Windows;
using System.Windows.Controls;

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

        #region event for getting selected items from datagrid
        public static bool GetSelectedItemsDatagridEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableSelectedItemsDatagridProperty);
        }

        public static void SetSelectedItemsDatagridEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableSelectedItemsDatagridProperty, value);
        }

        public static readonly DependencyProperty EnableSelectedItemsDatagridProperty = DependencyProperty.RegisterAttached("EnableSelectedItemsDatagridEvents", typeof(bool), typeof(EventManager), new PropertyMetadata(false, EnableSelectedItemsDatagridChanged));

        private static void EnableSelectedItemsDatagridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dataGrid)
            {
                dataGrid.SelectionChanged += (s, e) =>
                {
                   
                };
            }
        }
    }
    #endregion
}

