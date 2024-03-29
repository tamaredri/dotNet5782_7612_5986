﻿using BlApi;
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
using PO;

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
        public AddCustomer(IBL BLAccess, Customer customer)
        {
            InitializeComponent();
            BL = BLAccess;
            customerToCreate = customer;
            customerToCreate.LocationOfCustomer = new Location();
            DataContext = customerToCreate;

            #region reset values
            //IdTextBox.Clear();
            //NameTextBox.Clear();
            //PhoneTextBox.Clear();
            //LatitudeTextBox.Clear();
            //LongitudeTextBox.Clear();
            #endregion

            IdTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
            PhoneTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
            NameTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyLetters_PreviewKeyDown;
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

        private void ValueChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender is TextBox) && (sender as TextBox).IsInitialized)
                addButton.IsEnabled = !(IdTextBox.Text is "" || IdTextBox.Text is "0" || NameTextBox.Text is ""
                                        || PhoneTextBox.Text is"" || PhoneTextBox.Text is "0" || LatitudeTextBox.Text is ""
                                        || LatitudeTextBox.Text is "0" || LongitudeTextBox.Text is ""|| LongitudeTextBox.Text is "");
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.CreateCustomer(customerToCreate);
                this.Close();
            }
            catch(Exception x) { MessageBox.Show(x.Message); }

        }

        private void locationMap_Click(object sender, RoutedEventArgs e)
        {
            LocationWin locationWin = new();
            locationWin.ShowDialog();
        }
    }
}
