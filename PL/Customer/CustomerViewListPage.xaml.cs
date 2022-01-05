using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerViewListPage.xaml
    /// </summary>
    public partial class CustomerViewListPage : Page
    {
        IBL BLAccess;
        ObservableCollection<CustomerToList> listOfCustomer = new ObservableCollection<CustomerToList>();

        public CustomerViewListPage(IBL BLAccess)
        {
            InitializeComponent();
            this.BLAccess = BLAccess;
            listOfCustomer = new ObservableCollection<CustomerToList>(BLAccess.GetCustomerList());
            customersList.ItemsSource = listOfCustomer;
        }
    }
}
