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
using System.Windows.Threading;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntryPage2.xaml
    /// </summary>
    public partial class HomePageManager : Window
    {
        DispatcherTimer timer;

        IBL BL = BlFactory.GetBl();

        double panelWidth;
        bool hidden = true;
        public HomePageManager()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;
            sidePanel.Width = 60;

            ListFrame.Content = new HomeListPageManager();
        }




        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                sidePanel.Width += 5;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 5;
                if (sidePanel.Width <= 60)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) => timer.Start();

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        private void OpenLists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedIndex = ((ListView)sender).SelectedIndex;

            switch (selectedIndex)
            {
                case 0:
                    {
                        ListFrame.Content = new DronesList(BL);

                        break;
                    }
                case 1:
                    {
                        ListFrame.Content = new StationsList(BL);

                        break;
                    }
                case 2:
                    {
                        ListFrame.Content = new CustomersList(BL);

                        break;
                    }
                case 3:
                    {
                        ListFrame.Content = new ParcelsList(BL);

                        break;
                    }
                case 4:
                    {
                        ListFrame.Content = new HomeListPageManager();

                        break;
                    }
                default:
                    break;
            }
        }
    }
}

