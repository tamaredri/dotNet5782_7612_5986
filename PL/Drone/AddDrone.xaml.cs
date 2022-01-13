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
using PO;

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

            BatterySellector.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
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

        #region details of drone
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)=>
            addDrone.IsEnabled = ModelTextBox.Text is not "" && 
                                 weightCombobox.SelectedItem is not null && 
                                 stationCombobox.SelectedItem is not null;

           
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) =>
            addDrone.IsEnabled = ModelTextBox.Text is not "" && 
                                 weightCombobox.SelectedItem is not null && 
                                 stationCombobox.SelectedItem is not null;
        #endregion


        private void addDrone_Click(object sender, RoutedEventArgs e)
        {
            //create the drone
            try
            {
                droneToCreate.MaxWeight = (WeightCategories)weightCombobox.SelectedItem;
                int idStation = (stationCombobox.SelectedItem as StationToList).ID;
                BL.CreateDrone(droneToCreate, idStation);
                this.Close();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }

        }

        private void DroneBattery_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DroneBattery.Value = ((int)DroneBattery.Value);
        }
    }
}