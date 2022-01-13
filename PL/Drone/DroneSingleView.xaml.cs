using BlApi;
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
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneSingleView.xaml
    /// </summary>
    
    public partial class DroneSingleView : Window
    {
        IBL BL;
        DronePO droneToShow = new();

        public DroneSingleView(IBL BLAccess, int droneID)
        {
            InitializeComponent();
            BL = BLAccess;
            droneBOToPO(ref droneToShow, BL.GetDrone(droneID));
            DataContext = droneToShow;
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }

        #region copy droneBO to dronePO
        private void droneBOToPO(ref DronePO dronePO, Drone droneBO)
        {

            dronePO = new()
            {
                ID = droneBO.ID,
                Model = droneBO.Model,
                Battery = droneBO.Battery,
                Status = droneBO.Status,
                Weight = droneBO.MaxWeight,
                Location = new() { Lattitude = droneBO.DroneLocation.Lattitude, Longitude = droneBO.DroneLocation.Longitude },
                ParcelInDeliveryByDrone = (droneBO.ParcelInDeliveryByDrone != null) ? new()
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
                }:null
            };
            

        }
        #endregion


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

        #region update
        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.UpdateDrone(droneToShow.ID, ModelTextBox.Text);
            }catch(Exception x) { MessageBox.Show(x.Message); }
            droneToShow.Model = ModelTextBox.Text;
        }


        #endregion

        private void parcel_Click(object sender, RoutedEventArgs e)
        {
            ParcelSingleView parcelSingleView = new(BL,droneToShow.ParcelInDeliveryByDrone.ID);
            parcelSingleView.ShowDialog();
        }

        private void sentToCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.SendToCharge(droneToShow.ID);
            }catch(Exception x) { MessageBox.Show(x.Message); }
            droneBOToPO (ref droneToShow, BL.GetDrone(droneToShow.ID));
            DataContext = droneToShow;
        }

        private void schedualToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.PairDroneParcel(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            droneBOToPO(ref droneToShow, BL.GetDrone(droneToShow.ID));
            DataContext = droneToShow;
        }

        private void pickUpByParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.PickUpParcelByDrone(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            droneBOToPO(ref droneToShow, BL.GetDrone(droneToShow.ID));
            DataContext = droneToShow;
        }

        private void delivereByParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.DeliverParcel(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            droneBOToPO(ref droneToShow, BL.GetDrone(droneToShow.ID));
            DataContext = droneToShow;
        }

        private void automaticState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void releaseFromCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.ReleaseFromCharge(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            droneBOToPO(ref droneToShow, BL.GetDrone(droneToShow.ID));
            DataContext = droneToShow;
        }


    }
}
