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
    /// Interaction logic for StationListView.xaml
    /// </summary>
    public partial class StationListView : Page
    {
        IBL BLAccess;
        ObservableCollection<StationToList> listOfStations = new ObservableCollection<StationToList>();
        public StationListView(IBL BLAccess)
        {
            InitializeComponent();

            this.BLAccess = BLAccess;
            listOfStations = new ObservableCollection<StationToList>(BLAccess.GetStationList());
            StationsList.ItemsSource = listOfStations;

            InChargeSelector.ItemsSource = (from Station in listOfStations
                                            where Station.UsedChargeSlots >= 0
                                            select Station.UsedChargeSlots).ToList();

            AvailableChargeSelector.ItemsSource = (from Station in listOfStations
                                                   where Station.AvailableChargeSlots >= 0
                                                   select Station.AvailableChargeSlots).ToList();
        }

        private void OpenStationDetails_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            StationSingleView single = new StationSingleView(BLAccess, (StationToList)((DataGrid)sender).SelectedItem);
            
        }

        private void InCharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //grouping
            //StationsList.ItemsSource = (from station in StationsList
            //                            where station.UsedChargeSlots == )
        }
    }
}
