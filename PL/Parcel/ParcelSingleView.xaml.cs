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
    /// Interaction logic for ParcelSingleView.xaml
    /// </summary>
    public partial class ParcelSingleView : Window
    {
        IBL BL;
        ParcelPO parcelPO = new ParcelPO();
        public ParcelSingleView(IBL BLAccess, int ID)
        {
            InitializeComponent();
            BL = BLAccess;
            ParcelBOToPO(ref parcelPO, BL.GetParcel(ID));

            DataContext = parcelPO;
            DroneConnectedToTheParcel.DataContext = parcelPO.DroneToDeliverParcel;
        }

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


        #region copy BO to PO
        void ParcelBOToPO(ref ParcelPO parcelPO, Parcel parcelBO)
        {
            parcelPO = new() 
            {
                ID = parcelBO.ID, 
                CreateTime = parcelBO.CreateTime, 
                DelivereTime = parcelBO.DelivereTime, 
                DroneToDeliverParcel = parcelBO.DroneToDeliverParcel, PickUpTime = parcelBO.PickUpTime,
                ParcelPriorities = parcelBO.ParcelPriorities, 
                ScheduleTime = parcelBO.ScheduleTime, 
                Sender = parcelBO.Sender, 
                Target = parcelBO.Target, 
                Weight = parcelBO.Weight, 
                IsPaired = parcelBO.ScheduleTime is not null
            };
        }
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
        #endregion

        private void OpenDroneFromParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DronePO dronePO = DroneBOToPOList(BL.GetDrone(parcelPO.DroneToDeliverParcel.ID));
                DroneSingleView droneSingleView = new DroneSingleView(BL,dronePO);
                droneSingleView.ShowDialog();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }
    }
}
