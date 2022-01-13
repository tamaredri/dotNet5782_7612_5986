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
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Page
    {
        private IBL BL;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CustomerToList> cList;
        public ObservableCollection<CustomerToList> CList
        {
            get { return cList; }
            set 
            {
                cList = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CList"));
                }
            }
        }


        public CustomersList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            CList = new ObservableCollection<CustomerToList>(BL.GetCustomerList());
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

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customerToCreate = new();
            AddCustomer addCustomer = new(BL, customerToCreate);
            addCustomer.ShowDialog();

            CustomerToList customerToList = BL.GetPartOfCustomer(x => customerToCreate.ID == x.ID).FirstOrDefault();
            if (customerToList is not default(CustomerToList)) CList.Add(customerToList); //the customer was added to the list
        }

        private void OpenCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerSingleViewWin customerSingleViewWin = new CustomerSingleViewWin(BL, ((sender as DataGrid).SelectedItem as CustomerToList));
            customerSingleViewWin.ShowDialog();
        }
    }
}
