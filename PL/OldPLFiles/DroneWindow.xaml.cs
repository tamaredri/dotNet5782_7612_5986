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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneWindow.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        internal readonly IBL BLAccess;
        private BO.DroneToList droneToUpDate;


        


        //-----------------------------------------------add-Drone-----------------------------------------
        #region add drone
        public DroneWindow(IBL BlAccessDronesListWindow)
        {
            InitializeComponent();
            BLAccess = BlAccessDronesListWindow;
            //List<IBL.BO.StationToList> s = BLAccess.GetPartOfStation(x => x.AvailableChargeSlots > 0).ToList();
            //if (s.Count() == 0)
            //{
            //    MessageBox.Show("there are no station available.\ntry again later", "no station available", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    this.Close();
            //}


            //------------ID--------------
            DroneID.Text = (BLAccess.GetDroneRunnindNumber() + 1).ToString();

            //-----------MaximumWeight---------
            MaximumWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            //----------Status----------------
            DroneStatus.Text = DroneStatuses.maintenance.ToString();
            DroneStatus.IsReadOnly = true;

            //-----------StationToCharge-------
            StationToCharge.ItemsSource = BLAccess.GetPartOfStation(x => x.AvailableChargeSlots > 0);

          
            //--------------update----------------
            UpdateDrone.Visibility = Visibility.Hidden;
            UpdateDrone.IsEnabled = false;

            //options available: pick up
            delivereParcelByDrone.Visibility = Visibility.Hidden;
            delivereParcelByDrone.IsEnabled = false;
            pickUpParcelByDrone.Visibility = Visibility.Hidden;
            pickUpParcelByDrone.IsEnabled = false;
            scheduleParcelToDrone.Visibility = Visibility.Hidden;
            scheduleParcelToDrone.IsEnabled = false;
            releaseDroneFromCharge.Visibility = Visibility.Hidden;
            releaseDroneFromCharge.IsEnabled = false;
            SendDroneToCharge.Visibility = Visibility.Hidden;
            SendDroneToCharge.IsEnabled = false;

            CancleRleaseFromCharge.Visibility = Visibility.Hidden;
            send.Visibility = Visibility.Hidden;
            TimeChargeValue.Visibility = Visibility.Hidden;
            TimeChargeLable.Visibility = Visibility.Hidden;
        }


        private void StationToCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneModel.Text != "" && MaximumWeightSelector.SelectedItem != null && StationToCharge.SelectedItem != null)
                AddTheDrone.IsEnabled = true;
            else AddTheDrone.IsEnabled = false;
        }

        private void MaximumWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationToCharge.SelectedItem != null && DroneModel.Text != null)
                AddTheDrone.IsEnabled = true;
            else AddTheDrone.IsEnabled = false;
        }
        private void DroneBattery_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BatterySelection.Text = ((int)DroneBattery.Value).ToString();
        }

        private void AddTheDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Drone droneToAdd = new()
                {
                    Model = DroneModel.Text,
                    Battery = (int)DroneBattery.Value,
                    Weight = (WeightCategories)MaximumWeightSelector.SelectedItem
                };


                BLAccess.CreateDrone(droneToAdd, ((BO.StationToList)StationToCharge.SelectedItem).ID);

                this.Close();

                MessageBox.Show("The drone was added successfully!");

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
                AddTheDrone.IsEnabled = false;
                //this.Close();
            }



            //DroneListWindow listWin = new DroneListWindow(BLAccess);
            //make it close the past window and open it when it closes this window


        }
        #endregion

        //-----------------------------------------------up-Date-Drone-----------------------------------------
        #region update drone
        public DroneWindow(BO.DroneToList droneToUpDateList, IBL BlAccessDronesListWindow)
        {
            InitializeComponent();
            BLAccess = BlAccessDronesListWindow;
            droneToUpDate = droneToUpDateList;
            reloadData();
            //------------add----------------
            AddTheDrone.Visibility = Visibility.Hidden;
            AddTheDrone.IsEnabled = false;
            //--------------update-------------
            CancleRleaseFromCharge.Visibility = Visibility.Hidden;
            send.Visibility = Visibility.Hidden;
            TimeChargeValue.Visibility = Visibility.Hidden;
            TimeChargeLable.Visibility = Visibility.Hidden;

            if (droneToUpDate.Status == DroneStatuses.available)
            {
                //options available: send to charge / schedule parcel
                availableStats();
            }
            else if (droneToUpDate.Status == DroneStatuses.maintenance)
            {
                //options available: release from charge
                maintenanceStats();
            }
            else if (droneToUpDate.Status == DroneStatuses.delivery)
            {
                Parcel parcelInDelivery = new();
                try
                {parcelInDelivery = BLAccess.GetParcel(droneToUpDate.ParcelId);}
                catch(ContradictoryDataExeption x)
                {MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error);}

                if (parcelInDelivery.PickUpTime == null)
                {/*options available: pick up*/ pickUpStats();}
                else
                {/*options available: delivere*/  delivereStats();}
            }

            /*
             * delivereParcelByDrone
             * pickUpParcelByDrone
             * scheduleParcelToDrone
             * releaseDroneFromCharge
             * SendDroneToCharge
             * 
             * CancleRleaseFromCharge
             * send
             * TimeChargeValue
             * TimeChargeLable
             */

        }

        private void charge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.SendToCharge(droneToUpDate.ID);

                //options available: release from charge
                maintenanceStats();
                reloadData();

                MessageBox.Show($"{droneToUpDate.Model} is now charging!", "SUCCESS", MessageBoxButton.OK);
            }
            catch (BattaryExeption x)
            { MessageBox.Show(x.Message + "\ntalk to your manager", "BATTERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception x)
            { MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        #region send to charge
        private void Release_Click(object sender, RoutedEventArgs e)
        {
            TimeChargeLable.Visibility = Visibility.Visible;
            TimeChargeValue.Visibility = Visibility.Visible;
            TimeChargeValue.IsEnabled = true;
            send.Visibility = Visibility.Visible;
            CancleRleaseFromCharge.IsEnabled = true;
            CancleRleaseFromCharge.Visibility = Visibility.Visible;
            releaseDroneFromCharge.IsEnabled = false;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.ReleaseFromCharge(droneToUpDate.ID, double.Parse(TimeChargeValue.Text));
               
                DroneBattery.Value = BLAccess.GetDrone(droneToUpDate.ID).Battery;
                //change options for the drone
                availableStats();
                TimeChargeLable.Visibility = Visibility.Hidden;
                TimeChargeValue.Visibility = Visibility.Hidden;
                TimeChargeValue.IsEnabled = false;
                send.Visibility = Visibility.Hidden;
                send.IsEnabled = false;
                CancleRleaseFromCharge.IsEnabled = false;
                CancleRleaseFromCharge.Visibility = Visibility.Hidden;
                reloadData();

                MessageBox.Show($"{droneToUpDate.Model} was released from charge successfully!", "SUCCESS", MessageBoxButton.OK);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TimeChargeValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimeChargeValue.Text != "")
                send.IsEnabled = true;
            else send.IsEnabled = false;
        }

        private void CancleRleaseFromCharge_Click(object sender, RoutedEventArgs e)
        {
            maintenanceStats();
            TimeChargeLable.Visibility = Visibility.Hidden;
            TimeChargeValue.Visibility = Visibility.Hidden;
            TimeChargeValue.IsEnabled = false;
            send.Visibility = Visibility.Hidden;
            send.IsEnabled = false;
            CancleRleaseFromCharge.IsEnabled = false;
            CancleRleaseFromCharge.Visibility = Visibility.Hidden;

            MessageBox.Show($"that is a good choice.\nlet {droneToUpDate.Model} rest. he worked hard lately","SUCCESS", MessageBoxButton.OK);
        }
        #endregion

        private void scheduleParcelToDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.PairDroneParcel(droneToUpDate.ID);
                pickUpStats();
                reloadData();

                MessageBox.Show($"parcel ({droneToUpDate.ParcelId}) is now paired to {droneToUpDate.Model}", "SUCCESS", MessageBoxButton.OK);

            }
            catch (BattaryExeption x)
            { 
                MessageBox.Show(x.Message + "\nrecommendation: send the drone to charge", "BATTERY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                //only open the charge option
            }
            catch(Exception x)
            { MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void pickUpParcelByDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.PickUpParcelByDrone(droneToUpDate.ID);
                delivereStats();
                reloadData();

                MessageBox.Show($"parcel ({droneToUpDate.ParcelId}) hed been picked up by {droneToUpDate.Model}!", "SUCCESS", MessageBoxButton.OK);
            }
            catch (Exception x)
            { MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void delivereParcelByDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.DeliverParcel(droneToUpDate.ID);
                availableStats();
                reloadData();

                MessageBox.Show($"parcel ({droneToUpDate.ParcelId}) reached his destination!\ngood job!", "SUCCESS", MessageBoxButton.OK);
            }
            catch (Exception x)
            { MessageBox.Show(x.Message, "UNEXPECTED ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        #endregion
        private void DroneModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddTheDrone.Visibility != Visibility.Hidden)
            {
                if (StationToCharge.SelectedItem != null && MaximumWeightSelector.SelectedItem != null && DroneModel.Text != "")
                    AddTheDrone.IsEnabled = true;
                else AddTheDrone.IsEnabled = false;
            }
            else
            {
                if (DroneModel.Text != "") 
                    UpdateDrone.IsEnabled = true;
                else
                    UpdateDrone.IsEnabled = false;
            }
        }

        private void closeDroneWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BLAccess.UpdateDrone(droneToUpDate.ID, DroneModel.Text);
                this.Close();
                MessageBox.Show($"model updated sucssfully.\nthe new model: {droneToUpDate.Model}");
            }
            catch(DoesntExistExeption x)
            {
                MessageBox.Show(x.Message, "UNEXPECTED ERROR" ,MessageBoxButton.OK, MessageBoxImage.Error);
                UpdateDrone.IsEnabled = false;
            }
        }

        #region update abailablility options

        private void availableStats()
        {
            //options available: send to charge / schedule parcel
            delivereParcelByDrone.Visibility = Visibility.Hidden;
            delivereParcelByDrone.IsEnabled = false;
            pickUpParcelByDrone.Visibility = Visibility.Hidden;
            pickUpParcelByDrone.IsEnabled = false;
            scheduleParcelToDrone.Visibility = Visibility.Visible;
            scheduleParcelToDrone.IsEnabled = true;
            releaseDroneFromCharge.Visibility = Visibility.Hidden;
            releaseDroneFromCharge.IsEnabled = false;
            SendDroneToCharge.Visibility = Visibility.Visible;
            if (DroneBattery.Value < 100)
            {SendDroneToCharge.IsEnabled = true;}
        }
        private void maintenanceStats()
        {
            //options available: release from charge
            delivereParcelByDrone.Visibility = Visibility.Hidden;
            delivereParcelByDrone.IsEnabled = false;
            pickUpParcelByDrone.Visibility = Visibility.Hidden;
            pickUpParcelByDrone.IsEnabled = false;
            scheduleParcelToDrone.Visibility = Visibility.Hidden;
            scheduleParcelToDrone.IsEnabled = false;
            releaseDroneFromCharge.Visibility = Visibility.Visible;
            releaseDroneFromCharge.IsEnabled = true;
            SendDroneToCharge.Visibility = Visibility.Hidden;
            SendDroneToCharge.IsEnabled = false;
        }
        private void delivereStats()
        {
            //options available: delivere
            delivereParcelByDrone.Visibility = Visibility.Visible;
            delivereParcelByDrone.IsEnabled = true;
            pickUpParcelByDrone.Visibility = Visibility.Hidden;
            pickUpParcelByDrone.IsEnabled = false;
            scheduleParcelToDrone.Visibility = Visibility.Hidden;
            scheduleParcelToDrone.IsEnabled = false;
            releaseDroneFromCharge.Visibility = Visibility.Hidden;
            releaseDroneFromCharge.IsEnabled = false;
            SendDroneToCharge.Visibility = Visibility.Hidden;
            SendDroneToCharge.IsEnabled = false;
        }
        private void pickUpStats()
        {
            //options available: pick up
            delivereParcelByDrone.Visibility = Visibility.Hidden;
            delivereParcelByDrone.IsEnabled = false;
            pickUpParcelByDrone.Visibility = Visibility.Visible;
            pickUpParcelByDrone.IsEnabled = true;
            scheduleParcelToDrone.Visibility = Visibility.Hidden;
            scheduleParcelToDrone.IsEnabled = false;
            releaseDroneFromCharge.Visibility = Visibility.Hidden;
            releaseDroneFromCharge.IsEnabled = false;
            SendDroneToCharge.Visibility = Visibility.Hidden;
            SendDroneToCharge.IsEnabled = false;
        }
        
        private void reloadData()
        {
            //------------ID--------------
            DroneID.Text = droneToUpDate.ID.ToString();
            //-----------battery----------
            DroneBattery.Value = droneToUpDate.Battery;
            DroneBattery.IsEnabled = false;
            //-----------MaximumWeight---------
            MaximumWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            MaximumWeightSelector.SelectedItem = droneToUpDate.Weight;
            MaximumWeightSelector.IsEnabled = false;
            //----------Status----------------
            DroneStatus.Text = droneToUpDate.Status.ToString();
            DroneStatus.IsReadOnly = true;
            //-----------StationToCharge-------
            if (droneToUpDate.Status == DroneStatuses.maintenance)
            {
                //doesnt work!!!
                StationToCharge.ItemsSource = BLAccess.GetPartOfStation(x=> x.AvailableChargeSlots > 0);
                StationToCharge.SelectedItem = BLAccess.GetPartOfStation(x=> x.Name == BLAccess.GetTheStationCharge(droneToUpDate.ID)).FirstOrDefault(); //StationToCharge.ItemsSource.GetEnumerator().MoveNext().ToString();
            }
            StationToCharge.IsEnabled = false;
            //------------Model--------------
            DroneModel.Text = droneToUpDate.Model;
        }

        #endregion
    }
}
