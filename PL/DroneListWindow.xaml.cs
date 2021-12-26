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
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        internal readonly IBL BLAccess;
        public DroneListWindow(IBL BLAccessFromMainWin)
        {
            
            BLAccess = BLAccessFromMainWin;
            InitializeComponent();
            DroneListView.ItemsSource = BLAccess.GetDroneList();
            
            StatusSelector.Items.Add("");
            Array statuses = Enum.GetValues(typeof(DroneStatuses));
            for (int i = 0; i < statuses.Length; i++)
            {
                StatusSelector.Items.Add(statuses.GetValue(i));
            }
            //StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.Items.Add("");
            Array weight = Enum.GetValues(typeof(WeightCategories));
            for (int i = 0; i < weight.Length; i++)
            {
                WeightSelector.Items.Add(weight.GetValue(i));
            }
            //WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StatusSelector.SelectedItem = "";
            WeightSelector.SelectedItem = "";
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DroneToList> dronesList = new();
            if (StatusSelector.SelectedItem.ToString() == "") {
                //dronesList = BLAccess.GetDroneList().ToList();
                //BLAccess.GetDroneList().ToList().Sort((x, y) =>{
                //if (x.ID > y.ID)
                //    return 1;
                //if (x.ID < y.ID)
                //    return -1;
                //else return 0; } );
                DroneListView.ItemsSource = BLAccess.GetDroneList().ToList();
            }
            else
                DroneListView.ItemsSource = BLAccess.GetPartOfDrone(x => x.Status == (DroneStatuses)StatusSelector.SelectedItem);
                //    = BLAccess.GetPartOfDrone(x =>
                //{
                //    if (WeightSelector.SelectedItem.ToString() != "")
                //        return (x.Status == (DroneStatuses)StatusSelector.SelectedItem) && (x.Weight == (WeightCategories)WeightSelector.SelectedItem);
                //    else
                //        return x.Status == (DroneStatuses)StatusSelector.SelectedItem;
                //});
                
        }    

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightSelector.SelectedItem.ToString() == "")
                DroneListView.ItemsSource = BLAccess.GetDroneList();
            else
                DroneListView.ItemsSource = BLAccess.GetPartOfDrone(x => x.Weight == (WeightCategories)WeightSelector.SelectedItem /*&& x.Status == (DroneStatuses)StatusSelector.SelectedItem*/);
        }

        private void AddDroneButten_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow droneWin = new DroneWindow(BLAccess);
            //this.Close();
            droneWin.ShowDialog();
            DroneListView.ItemsSource = BLAccess.GetDroneList();
            StatusSelector.SelectedItem = "";
            WeightSelector.SelectedItem = "";
        }

        private void update_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneWindow droneWin = new DroneWindow((DroneToList)DroneListView.SelectedItem, BLAccess);
            droneWin.ShowDialog();
            StatusSelector.SelectedItem = "";
            WeightSelector.SelectedItem = "";

            List <DroneToList> droneList= BLAccess.GetDroneList().ToList();
            droneList.Sort((x, y) => {
                if (x.ID > y.ID)
                    return 1;
                if (x.ID < y.ID)
                    return -1;
                else return 0;
            });
            DroneListView.ItemsSource = droneList;
            StatusSelector.SelectedItem = "";
            WeightSelector.SelectedItem = "";
        }

        private void Close_Click(object sender, RoutedEventArgs e) { this.Close(); }

       
    }
}
