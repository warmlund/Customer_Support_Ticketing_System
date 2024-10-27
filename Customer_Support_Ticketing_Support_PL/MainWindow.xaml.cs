using Customer_Support_Ticketing_System_PL;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Customer_Support_Ticketing_System_BLL;

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
            _customerSupportBLL = new CustomerSupportBLL();
            DataContext = new ViewModel(_customerSupportBLL);
        }
    }
}