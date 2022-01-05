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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListViewPageMannager.xaml
    /// </summary>
    public partial class ListViewPageMannager : Window
    {
        IBL BLAccess;
        public ListViewPageMannager(IBL BLAccess)
        {
            InitializeComponent();
            this.BLAccess = BLAccess;
        }

        private void OpenListOfStations_Click(object sender, RoutedEventArgs e)
        {
            ListPage.Content = new StationListView(BLAccess);
        }
    }
}
