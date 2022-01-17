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
using System.ComponentModel;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneSingleView.xaml
    /// </summary>
    
    public partial class DroneSingleView : Window
    {
        #region automatic simulator  

        BackgroundWorker worker;
        private void updateDrone() => worker.ReportProgress(0); //update the presentation
        private bool checkStop() => worker.CancellationPending; //check if the 
        bool auto = false;
        bool closing = false;
        #endregion

        IBL BL;
        DronePO droneToShow = new();

        public DroneSingleView(IBL BLAccess, DronePO dronePO)
        {
            InitializeComponent();
            BL = BLAccess;
            droneToShow = dronePO;
            Drone droneBO = BL.GetDrone(droneToShow.ID);

            //update droneindelivery
            droneToShow.ParcelInDeliveryByDrone = (droneBO.ParcelInDeliveryByDrone != null) ? new()
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
            } : null;
            if (droneBO.ParcelInDeliveryByDrone != null)
            {
                senderName.Text = droneBO.ParcelInDeliveryByDrone.Sender.Name;
                targeName.Text = droneBO.ParcelInDeliveryByDrone.Target.Name;
                weight.Text = droneBO.ParcelInDeliveryByDrone.Weight.ToString();
            }
            DataContext = droneToShow;
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }

        #region copy droneBO to dronePO
        private void updateDroneToShow(Drone droneBO)
        {
            droneToShow.ID = droneBO.ID;
            droneToShow.Model = droneBO.Model;
            droneToShow.Battery = droneBO.Battery;
            droneToShow.Status = droneBO.Status;
            droneToShow.Weight = droneBO.MaxWeight;
            droneToShow.Location = new() { Lattitude = droneBO.DroneLocation.Lattitude, Longitude = droneBO.DroneLocation.Longitude };
            droneToShow.ParcelInDeliveryByDrone = (droneBO.ParcelInDeliveryByDrone != null) ? new()
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
            } : null;
            droneToShow.ParcelId = (droneBO.ParcelInDeliveryByDrone != null) ? droneToShow.ParcelInDeliveryByDrone.ID : 0;
            if (droneBO.ParcelInDeliveryByDrone != null)
            {
                senderName.Text = droneBO.ParcelInDeliveryByDrone.Sender.Name;
                targeName.Text = droneBO.ParcelInDeliveryByDrone.Target.Name;
                weight.Text = droneBO.ParcelInDeliveryByDrone.Weight.ToString();
            }
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
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (auto is true)
                closing = true;
            else
                this.Close();
        }
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

        #region drone actions
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
            Drone updateDrone = BL.GetDrone(droneToShow.ID);
            updateDroneToShow(BL.GetDrone(droneToShow.ID));
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }

        private void schedualToParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.PairDroneParcel(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            updateDroneToShow(BL.GetDrone(droneToShow.ID));
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }

        private void pickUpByParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.PickUpParcelByDrone(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            updateDroneToShow(BL.GetDrone(droneToShow.ID));
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }

        private void delivereByParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                BL.DeliverParcel(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            updateDroneToShow(BL.GetDrone(droneToShow.ID));
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;

        }

        private void releaseFromCharge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.ReleaseFromCharge(droneToShow.ID);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
            updateDroneToShow(BL.GetDrone(droneToShow.ID));
            droneInParcel.DataContext = droneToShow.ParcelInDeliveryByDrone;
        }
        #endregion

        #region simulator
        private void automaticState_Click(object sender, RoutedEventArgs e)
        {
            if (auto is false)
            {
                startAutomatic.Visibility = Visibility.Hidden;
                startAutomatic.IsEnabled = false;
                auto = true;
                worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync(droneToShow.ID);
            }
            else
            {
                startAutomatic.Visibility = Visibility.Visible;
                startAutomatic.IsEnabled = true;
                worker.CancelAsync(); //pend the cancelletion
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) =>
            updateDroneToShow(BL.GetDrone(droneToShow.ID));

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            auto = false;
            worker = null;
            if (closing) Close();
            startAutomatic.Visibility = Visibility.Visible;
            startAutomatic.IsEnabled = true;
            MessageBox.Show("the simulator stoped!\none of the following might have happend:\n1) stopped by force\n2) no parcels left for the drone to carry", "Simulator", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e) =>
            BL.StartSimulator((int)e.Argument, updateDrone, checkStop);

        #endregion

        private void locationMap_Click(object sender, RoutedEventArgs e)
        {
            double myLon = droneToShow.Location.Longitude;
            double myLat = droneToShow.Location.Lattitude;
            LocationWin locationWin = new(myLon,myLat);
            locationWin.ShowDialog();
        }
    }
}
