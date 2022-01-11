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

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerSingleViewWin.xaml
    /// </summary>
    public partial class CustomerSingleViewWin : Window
    {
        IBL BL;
        Customer CustomerToShow = new Customer();
        public CustomerSingleViewWin(IBL BLAccess, int customerID)
        {
            InitializeComponent();
            BL = BLAccess;
            CustomerToShow = BL.GetCustomer(customerID);
            DataContext = CustomerToShow;
            //OnTheWayList.DataContext = CustomerToShow.Recieved;
            OnTheWayList.ItemsSource = CustomerToShow.Recieved;
            //SentList.ItemsSource = CustomerToShow.Sent;
            SentList.ItemsSource = CustomerToShow.Sent;

            OnTheWayList.MouseDoubleClick += ShowParcel_MouseDoubleClick;
        }

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
    }
}
