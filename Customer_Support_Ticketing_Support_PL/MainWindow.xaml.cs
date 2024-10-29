using Customer_Support_Ticketing_System_BLL;
using Customer_Support_Ticketing_System_PL;
using System.Windows;

namespace Customer_Support_Ticketing_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerSupportBLL _customerSupportBLL;
        public MainWindow()
        {
            InitializeComponent();
            _customerSupportBLL = new CustomerSupportBLL(); //creates instance of the BLL layer
            DataContext = new ViewModel(_customerSupportBLL); //Sets the datacontext as viewmodel with the bll layer as an argument
        }
    }
}