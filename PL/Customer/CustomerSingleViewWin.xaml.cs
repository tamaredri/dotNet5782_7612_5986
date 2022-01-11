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
using System.Windows.Shapes;
using BlApi;
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerSingleViewWin.xaml
    /// </summary>
    public partial class CustomerSingleViewWin : Window
    {
        IBL BL;
        CustomerPO CustomerPO = new CustomerPO();
        Customer CustomerToShow = new Customer();

        public CustomerSingleViewWin(IBL BLAccess, int customerID)
        {
            InitializeComponent();
            BL = BLAccess;
            CustomerBOtoPO(ref CustomerPO, BL.GetCustomer(customerID));

            DataContext = CustomerPO;
            OnTheWayList.ItemsSource = CustomerToShow.Recieved;
            SentList.ItemsSource = CustomerToShow.Sent;

            OnTheWayList.MouseDoubleClick += ShowParcel_MouseDoubleClick;
        }

        #region copy customer from BL to PO
        void CustomerBOtoPO(ref CustomerPO customerPO, Customer customerBO)
        {
            customerPO = new CustomerPO()
            {
                ID = customerBO.ID,
                Name = customerBO.Name,
                Phone = customerBO.Phone,
                LocationOfCustomer = new Location()
                {
                    Lattitude = customerBO.LocationOfCustomer.Lattitude,
                    Longitude = customerBO.LocationOfCustomer.Longitude
                }, 
                Recieved = (from customer in customerBO.Recieved select customer).ToList(),
                Sent = (from customer in customerBO.Sent select customer).ToList()
            };
        }
        #endregion

        #region panel header
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        #endregion

        private void ShowParcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelSingleView parcelSingleView = new ParcelSingleView(BL, ((sender as ListView).SelectedItem as ParcelInCustomer).ID);
            parcelSingleView.ShowDialog();
        }

        #region update
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox phoneTextBox = sender as TextBox;
            Update.IsEnabled = !(NameTextBox.Text is "") || !(phoneTextBox.Text.Length != 10);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox phoneTextBox = sender as TextBox;
            Update.IsEnabled = !(NameTextBox.Text is "") || !(phoneTextBox.Text.Length != 10);
        }
        #endregion
    }
}
