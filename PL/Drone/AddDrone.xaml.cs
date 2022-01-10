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
        Drone droneToAdd;
        public AddDrone(IBL BLAcsses)
        {
            InitializeComponent();
            droneToAdd = new();
            BL = BLAcsses;
            
            droneToAdd.ID = BL.GetDroneRunnindNumber()+1;
            DataContext = droneToAdd;

            //station To Charge
            stationCombobox.ItemsSource = BL.GetPartOfStation(x => x.AvailableChargeSlots > 0);

        }
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        private void DroneBattery_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DroneBattery.Value = (int)DroneBattery.Value;
        }

        private void location_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LocationWin location = new();
            location.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            droneToAdd.Model = modelTextBox.Text;
            droneToAdd.MaxWeight = (WeightCategories)weightCombobox.SelectedItem;
            //droneToAdd.DroneLocation= stationCombobox

            droneToAdd.Battery = (int)DroneBattery.Value;
            //BL.CreateDrone()
        }
    }
}
