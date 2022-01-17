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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationSingleView.xaml
    /// </summary>
    public partial class StationSingleView : Window
    {
        IBL BL;
        StationPO stationToShow = new();
        public StationSingleView(IBL BLAccess, int stationID)
        {
            InitializeComponent();
            BL = BLAccess;
            stationBOToPO(ref stationToShow, BL.GetStation(stationID));
            DataContext = stationToShow;
            droneInCharge.ItemsSource = stationToShow.ChargedDrones;
        }

        #region copy BO to PO

        public DronePO DroneBOToPOList(Drone droneBO)
        {
            DronePO dronePO = new DronePO()
            {
                ID = droneBO.ID,
                Model = droneBO.Model,
                Battery = droneBO.Battery,
                Status = droneBO.Status,
                Weight = droneBO.MaxWeight,
                Location = new() { Lattitude = droneBO.DroneLocation.Lattitude, Longitude = droneBO.DroneLocation.Longitude },
                ParcelInDeliveryByDrone = droneBO.ParcelInDeliveryByDrone = (droneBO.ParcelInDeliveryByDrone != null) ? new()
                {
                    ID = droneBO.ParcelInDeliveryByDrone.ID,
                    Weight = droneBO.ParcelInDeliveryByDrone.Weight,
                    Distance = droneBO.ParcelInDeliveryByDrone.Distance,
                    PickUp = droneBO.ParcelInDeliveryByDrone.PickUp,
                    InDelivery = droneBO.ParcelInDeliveryByDrone.InDelivery,
                    Priority = droneBO.ParcelInDeliveryByDrone.Priority,
                    Destination = new()
                    {
                        Lattitude = droneBO.ParcelInDeliveryByDrone.Destination.Lattitude,
                        Longitude = droneBO.ParcelInDeliveryByDrone.Destination.Longitude
                    },
                    Sender = new() { ID = droneBO.ParcelInDeliveryByDrone.Sender.ID, Name = droneBO.ParcelInDeliveryByDrone.Sender.Name },
                    Target = new() { ID = droneBO.ParcelInDeliveryByDrone.Target.ID, Name = droneBO.ParcelInDeliveryByDrone.Target.Name }
                } : null,

            };
            dronePO.ParcelId = (droneBO.ParcelInDeliveryByDrone != null) ? droneBO.ParcelInDeliveryByDrone.ID : 0;
            return dronePO;
        }
        private void stationBOToPO(ref StationPO stationPO, Station stationBO)
        {
            stationPO = new()
            {
                ID = stationBO.ID,
                Name = stationBO.Name,
                StationLocation = new() { Lattitude = stationBO.StationLocation.Lattitude, Longitude = stationBO.StationLocation.Longitude },
                AvailableChargeSlots=stationBO.AvailableChargeSlots, 
                ChargedDrones = (from station in stationBO.ChargedDrones select station).ToList()
            };
        }
        #endregion

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

        private void ValueChanged_TextChanged(object sender, TextChangedEventArgs e) =>
            update.IsEnabled = NameTextBox.Text != "" || chargeTextBox.Text != "";

        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.UpdateStation(stationToShow.ID, (chargeTextBox.Text is not "")? int.Parse(chargeTextBox.Text):0, NameTextBox.Text);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            if (NameTextBox.Text is not "") { stationToShow.Name = NameTextBox.Text; NameTextBox.Clear(); }
            if (chargeTextBox.Text is not "") { stationToShow.AvailableChargeSlots = int.Parse(chargeTextBox.Text); chargeTextBox.Clear(); }
        }

        

        private void droneInCharge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                DronePO dronePO = DroneBOToPOList(BL.GetDrone(((sender as ListBox).SelectedItem as DroneInCharge).ID));
                DroneSingleView droneSingleView = new(BL, dronePO);
                droneSingleView.ShowDialog();
            }catch(Exception x) { MessageBox.Show(x.Message); }
        }

        private void locationMap_Click(object sender, RoutedEventArgs e)
        {
            double myLon = stationToShow.StationLocation.Longitude;
            double myLat = stationToShow.StationLocation.Lattitude;
            LocationWin locationWin = new(myLon, myLat);
            locationWin.ShowDialog();
        }

        
    }
}
