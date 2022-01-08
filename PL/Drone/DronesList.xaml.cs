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


            foreach (var item in BL.GetDroneList())
            {
                dronsList.Add(item);
            }

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
            //dronsList.Clear();
            //if (statusCombobox.SelectedItem.ToString() == "")
            //    DataContext=
            //else
            //{
                
            //    //List<DroneToList> dronsFronBl = BL.GetPartOfDrone(drone => drone.Status == (DroneStatuses)statusCombobox.SelectedItem).ToList();
            //    //foreach (var item in dronsFronBl)
            //    //{
            //    //    dronsList.Add(item);
            //    //}
            //}
        }
    }

}