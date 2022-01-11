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
            DroneBOToPO(ref droneToShow, BL.GetDrone(droneID));
            DataContext = droneToShow;
        }

        #region copy droneBO to dronePO
        public void DroneBOToPO(ref DronePO dronePO, Drone droneBO)
        {

            dronePO = new()
            {
                ID = droneBO.ID,
                Model = droneBO.Model,
                Battery = droneBO.Battery,
                Status = droneBO.Status,
                Weight = droneBO.MaxWeight,
                Location = new() { Lattitude = droneBO.DroneLocation.Lattitude, Longitude = droneBO.DroneLocation.Longitude },
                ParcelInDeliveryByDrone = new()
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
                }
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

        #region updata
        private void update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.UpdateDrone(droneToShow.ID, ModelTextBox.Text);
            }catch(Exception x) { MessageBox.Show(x.Message); }
            droneToShow.Model = ModelTextBox.Text;
        }

        #endregion


    }
}
