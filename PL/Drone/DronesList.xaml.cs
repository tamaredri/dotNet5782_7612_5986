using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesList.xaml
    /// </summary>
    public partial class DronesList : Page
    {
        IBL BL;
        ObservableCollection<DroneToList> dronsList = new();

        public DronesList(IBL BlAccess)
        {
            InitializeComponent();
            BL = BlAccess;

            ienumerableToObservable(BL.GetDroneList());

            DataContext = dronsList;
        }




        private void ConboboxStatus_open(object sender, EventArgs e)
        {
            statusCombobox.Items.Clear();
            statusCombobox.Items.Add("");
            IEnumerable<IGrouping<DroneStatuses, DroneToList>> grouping = (from drone in BL.GetDroneList()
                                                                           group drone by drone.Status into droneInfo
                                                                           select droneInfo).ToList();
            grouping.OrderBy(g => g.Key);
            foreach (var group in grouping)
            {
                statusCombobox.Items.Add(group.Key);
            }
        }

        private void ConboboxWeight_open(object sender, EventArgs e)
        {
            weightCombobox.Items.Clear();
            weightCombobox.Items.Add("");
            IEnumerable<IGrouping<WeightCategories, DroneToList>> grouping = (from drone in BL.GetDroneList()
                                                                              group drone by drone.Weight into droneInfo
                                                                              select droneInfo).ToList();
            grouping.OrderBy(g => g.Key);
            foreach (var group in grouping)
            {
                weightCombobox.Items.Add(group.Key);
            }
        }

        private void ConboboxBattery_open(object sender, EventArgs e)
        {
            batteryConbobox.Items.Clear();
            batteryConbobox.Items.Add("");
            IEnumerable<IGrouping<int, DroneToList>> grouping = (from drone in BL.GetDroneList()
                                                                 group drone by drone.Battery into droneInfo
                                                                 select droneInfo).ToList();
            grouping.OrderBy(g => g.Key);
            foreach (var group in grouping)
            {
                batteryConbobox.Items.Add(group.Key);
            }
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (statusCombobox.SelectedItem is DroneStatuses)
            {
                ienumerableToObservable(BL.GetPartOfDrone(drone => drone.Status == (DroneStatuses)statusCombobox.SelectedItem));
            }
            else if (statusCombobox.SelectedItem is "")
            {
                
                ienumerableToObservable(BL.GetDroneList());
            }
            weightCombobox.SelectedItem = "";
            batteryConbobox.SelectedItem = "";

        }
        private void weightCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (weightCombobox.SelectedItem is WeightCategories)
            {
                ienumerableToObservable(BL.GetPartOfDrone(drone => drone.Weight == (WeightCategories)weightCombobox.SelectedItem));
            }
            else if (weightCombobox.SelectedItem is "")
            {

                ienumerableToObservable(BL.GetDroneList());
            }
            statusCombobox.SelectedItem = "";
            batteryConbobox.SelectedItem = "";
        }

        private void batteryConbobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (batteryConbobox.SelectedItem is int)
            {
                ienumerableToObservable(BL.GetPartOfDrone(drone => drone.Battery == (int)batteryConbobox.SelectedItem));
            }
            else if (batteryConbobox.SelectedItem is "")
            {

                ienumerableToObservable(BL.GetDroneList());
            }
            statusCombobox.SelectedItem = "";
            weightCombobox.SelectedItem = "";
        }

        private void ienumerableToObservable(IEnumerable<DroneToList> dronsListToConvert) {
            dronsList.Clear();
            foreach (var drone in dronsListToConvert)
            {
                dronsList.Add(drone);
            }
        }

    }

}