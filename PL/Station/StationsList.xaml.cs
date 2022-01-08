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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Page
    {

        IBL BL;
        public StationsList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;

            ObservableCollection<StationToList> stationsList = new();

            foreach (var item in BL.GetStationList())
            {
                stationsList.Add(item);
            }

            DataContext = stationsList;
        }
    }
}
