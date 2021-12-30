﻿using System;
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


namespace PL
{
    /// <summary>
    /// Interaction logic for entryPage.xaml
    /// </summary>
    public partial class entryPage : Window
    {
        public entryPage()
        {
            IBL BLAccess = BlFactory.GetBl();
            InitializeComponent();
            IBL blaccess = BlFactory.GetBl();
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoToDronrsList(object sender, RoutedEventArgs e)
        {
            DronesListView dronesListViewWindow = new DronesListView();
            dronesListViewWindow.ShowDialog();
        }


        private void GoToCustomersrsList(object sender, RoutedEventArgs e)
        {
            CustomersListView customerListViewWindow = new CustomersListView();
            customerListViewWindow.ShowDialog();
        }
    }
}
