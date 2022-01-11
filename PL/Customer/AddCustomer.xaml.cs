using BlApi;
using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        IBL BL;
        Customer customerToCreate;
        Location customersLocation = new Location();
        public AddCustomer(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            customerToCreate = new();
            DataContext = customerToCreate;
        }

        #region header paanel
        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        #endregion


        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e) {  }
        //=> 
        //addButton.IsEnabled = !(idTextBox.Text is "" || nameTextBox.Text is "" 
        //|| PhoneTextBox.Text is"" || latitudeTextBox.Text is "" || LongitudeTextBox.Text is "");

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        //=> 
        //addButton.IsEnabled = !(idTextBox.Text is "" || nameTextBox.Text is "" 
        //|| PhoneTextBox.Text is"" || latitudeTextBox.Text is "" || LongitudeTextBox.Text is "");

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        //=> 
        //addButton.IsEnabled = !(idTextBox.Text is "" || nameTextBox.Text is "" 
        //|| PhoneTextBox.Text is"" || latitudeTextBox.Text is "" || LongitudeTextBox.Text is "");

        private void latitudeTextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        //=> 
        //addButton.IsEnabled = !(idTextBox.Text is "" || nameTextBox.Text is "" 
        //|| PhoneTextBox.Text is"" || latitudeTextBox.Text is "" || LongitudeTextBox.Text is "");

        private void LongitudeTextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        //=> 
        //addButton.IsEnabled = !(idTextBox.Text is "" || nameTextBox.Text is "" 
        //|| PhoneTextBox.Text is"" || latitudeTextBox.Text is "" || LongitudeTextBox.Text is "");

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                BL.CreateCustomer(customerToCreate);
                this.Close();
            }
            catch(Exception x) { MessageBox.Show(x.Message); }

        }
    }
}
