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


        #region copy droneBO to dronePO
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
        #endregion

        private void OpenDroneFromParcel_Click(object sender, RoutedEventArgs e)
        {
            DroneSingleView droneSingleView = new DroneSingleView(BL, (DroneConnectedToTheParcel.DataContext as DronePO));
            droneSingleView.ShowDialog();

        }
    }
}
