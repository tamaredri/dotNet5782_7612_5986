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
        public DronesList(IBL BlAccess)
        {
            InitializeComponent();
            BL = BlAccess;

            ObservableCollection<DroneToList> dronsList = new();

            foreach (var item in BL.GetDroneList())
            {
                dronsList.Add(item);
            }

            DataContext = dronsList;



            
            
        }

       

        private void ConboboxStatus_open(object sender, ContextMenuEventArgs e)
        {
            statusCombobox.Items.Add("Clear");

            IEnumerable<IGrouping<DroneStatuses, DroneToList>> grouping = (from drone in BL.GetDroneList()
                        group drone by drone.Status into droneInfo
                        select droneInfo).ToList();
            
        }
    }

}