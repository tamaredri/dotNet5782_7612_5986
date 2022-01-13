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
        CustomerPO CustomerPO;
        CustomerToList CustomerFromListWin;
        public CustomerSingleViewWin(IBL BLAccess, CustomerToList customer)
        {
            InitializeComponent();
            BL = BLAccess;
            CustomerFromListWin = customer;
            CustomerBOtoPO(ref CustomerPO, BL.GetCustomer(customer.ID));

            DataContext = CustomerPO;
            OnTheWayList.ItemsSource = CustomerPO.Recieved;
            SentList.ItemsSource = CustomerPO.Sent;

            NameTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyLetters_PreviewKeyDown;
            PhoneTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
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

        #region open the parcel a customer sent / recived
        private void ShowParcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelSingleView parcelSingleView = new ParcelSingleView(BL, ((sender as ListView).SelectedItem as ParcelInCustomer).ID);
            parcelSingleView.ShowDialog();
        }
        #endregion

        #region update
        private void ValueChanged_TextChanged(object sender, TextChangedEventArgs e) =>
            Update.IsEnabled = !(NameTextBox.Text is "") || !((sender as TextBox).Text.Length != 10);

        private void UpdateCustmer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.UpdateCustomer(CustomerPO.ID, PhoneTextBox.Text is not "" ? int.Parse(PhoneTextBox.Text) : 0, NameTextBox.Text);

                if (NameTextBox.Text is not "")
                    CustomerPO.Name = CustomerFromListWin.Name = NameTextBox.Text;
                if (PhoneTextBox.Text is not "")
                    CustomerPO.Phone = CustomerFromListWin.Phone = int.Parse(PhoneTextBox.Text);

                NameTextBox.Text = PhoneTextBox.Text = "";
            }
            catch(Exception x)
            { MessageBox.Show(x.Message, "EXCEPTION", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        #endregion
    }
}
