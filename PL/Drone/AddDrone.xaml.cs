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
    /// Interaction logic for AddDrone.xaml
    /// </summary>
    public partial class AddDrone : Window
    {
        IBL BL;
        Drone droneToCreate;
        public AddDrone(IBL BLAcsses)
        {
            InitializeComponent();
            droneToCreate = new();
            BL = BLAcsses;

            droneToCreate.ID = BL.GetDroneRunnindNumber()+1;
            DataContext = droneToCreate;

            //station To Charge
            stationCombobox.ItemsSource = BL.GetPartOfStation(x => x.AvailableChargeSlots > 0);
            weightCombobox.ItemsSource = Enum.GetValues(typeof(WeightCategories));

        }

        #region panel events
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();
        #endregion

        #region details of dron
        private void DroneBattery_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DroneBattery.Value = (int)DroneBattery.Value;
            if (modelTextBox.Text != "" && weightCombobox.SelectedItem != null && stationCombobox.SelectedItem != null)
                addDrone.IsEnabled = true;
            else addDrone.IsEnabled = false;
        }

        private void modelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (modelTextBox.Text != "" && weightCombobox.SelectedItem != null && stationCombobox.SelectedItem != null)
                addDrone.IsEnabled = true;
            else addDrone.IsEnabled = false;
        }

        private void weightCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelTextBox.Text != "" && weightCombobox.SelectedItem != null && stationCombobox.SelectedItem != null)
                addDrone.IsEnabled = true;
            else addDrone.IsEnabled = false;
        }

        private void stationCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modelTextBox.Text != "" && weightCombobox.SelectedItem != null && stationCombobox.SelectedItem != null)
                addDrone.IsEnabled = true;
            else addDrone.IsEnabled = false;
        }
        #endregion


        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            droneToCreate.MaxWeight = (WeightCategories)weightCombobox.SelectedItem;
            int idStation = (stationCombobox.SelectedItem as StationToList).ID;
            //create the drone
            try
            {
                BL.CreateDrone(droneToCreate, idStation);
                this.Close();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }

        }
    }
}