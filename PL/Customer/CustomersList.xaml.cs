using System;
using System.Collections.Generic;
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
using BO;
using BlApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Page
    {
        IBL BL;
        ObservableCollection<CustomerToList> CList = new ObservableCollection<CustomerToList>();
        public CustomersList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;

            ienumerableToObservable(BL.GetCustomerList());

            DataContext = CList;

        }

        private void ienumerableToObservable(IEnumerable<CustomerToList> customerListToConvert)
        {
            CList.Clear();
            foreach (var customer in customerListToConvert)
            {
                CList.Add(customer);
            }
        }

        private void addCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new(BL);
            addCustomer.ShowDialog();
            ienumerableToObservable(BL.GetCustomerList());


        }
    }
}
