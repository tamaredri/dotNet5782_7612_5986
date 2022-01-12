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

        #region copy station BO to PO
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
            DroneSingleView droneSingleView = new(BL, ((sender as ListBox).SelectedItem as DroneInCharge).ID);
            droneSingleView.ShowDialog();
        }
    }
}
