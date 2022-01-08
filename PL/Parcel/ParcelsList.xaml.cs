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
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    public partial class ParcelsList : Page
    {
        IBL BL;
        public ParcelsList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;

            ObservableCollection<ParcelToList> parcelsList = new();

            foreach (var item in BL.GetParcelList())
            {
                parcelsList.Add(item);
            }

            DataContext = parcelsList;
        }
    }
}
