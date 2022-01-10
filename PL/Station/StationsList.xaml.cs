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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Page
    {

        IBL BL;
        ObservableCollection<StationToList> stationsList = new ObservableCollection<StationToList>();
        public StationsList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            iEnumerableToObservable(BL.GetStationList());
            DataContext = stationsList;
        }

        #region comboBox
        /// <summary>
        /// initialize the available comboBox with the values according to the list of stations
        /// </summary>
        private void TakenItems_DropDownOpened(object sender, EventArgs e)
        {
            TakenChargeSlotsComboBox.Items.Clear();
            TakenChargeSlotsComboBox.Items.Add("");
            IEnumerable<IGrouping<int, StationToList>> groupings = groupByTakenChargeSlots(BL.GetStationList());
            groupings = groupings.OrderBy(g => g.Key);
            foreach (var group in groupings)
                TakenChargeSlotsComboBox.Items.Add(group.Key);
        }

        /// <summary>
        /// initialize the taken comboBox with the values according to the list of stations
        /// </summary>
        private void AvailableItems_DropDownOpened(object sender, EventArgs e)
        {
            AvailableChargeSlotsComboBox.Items.Clear();
            AvailableChargeSlotsComboBox.Items.Add("");
            IEnumerable<IGrouping<int, StationToList>> groupings = groupByAvailableChargeSlots(BL.GetStationList());
            groupings = groupings.OrderBy(g => g.Key);
            foreach (var group in groupings)
                AvailableChargeSlotsComboBox.Items.Add(group.Key);
        }
        #endregion

        #region group
        /// <summary>
        /// group the list of stations according to the available charge slots in eace station in the list
        /// </summary>
        /// <param name="listToGroup">the list to group by</param>
        /// <returns>IEnumerable with the groups</returns>
        IEnumerable<IGrouping<int, StationToList>> groupByAvailableChargeSlots(IEnumerable<StationToList> listToGroup)
        => (from charge in listToGroup
            group charge by charge.AvailableChargeSlots into chargeInfo
            select chargeInfo).ToList();

        /// <summary>
        /// group the list of stations according to the used charge slots in eace station in the list
        /// </summary>
        /// <param name="listToGroup">the list to group by</param>
        /// <returns>IEnumerable with the groups</returns>
        IEnumerable<IGrouping<int, StationToList>> groupByTakenChargeSlots(IEnumerable<StationToList> listToGroup)
           => (from charge in listToGroup
                    group charge by charge.UsedChargeSlots into chargeInfo
                    select chargeInfo).ToList();
        #endregion

        #region selection changed - comboBox
        private void Taken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TakenChargeSlotsComboBox.SelectedItem is int)
                iEnumerableToObservable(BL.GetPartOfStation(station => station.UsedChargeSlots == (int)TakenChargeSlotsComboBox.SelectedItem));
            else if (TakenChargeSlotsComboBox.SelectedItem is "")
                iEnumerableToObservable(BL.GetStationList());
            AvailableChargeSlotsComboBox.SelectedItem = "";
        }

        private void Available_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AvailableChargeSlotsComboBox.SelectedItem is int)
                iEnumerableToObservable(BL.GetPartOfStation(station => station.AvailableChargeSlots == (int)AvailableChargeSlotsComboBox.SelectedItem));

            else if (AvailableChargeSlotsComboBox.SelectedItem is "")
                iEnumerableToObservable(BL.GetStationList());
            TakenChargeSlotsComboBox.SelectedItem = "";
        }
        #endregion

        /// <summary>
        /// convert from ienumerable to an observable collection
        /// </summary>
        /// <param name="listTOConvert">IEnumerable to convert</param>
        private void iEnumerableToObservable(IEnumerable<StationToList> listTOConvert)
        {
            stationsList.Clear();
            foreach (var station in listTOConvert)
                stationsList.Add(station);
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation add = new AddStation(BL);
            add.ShowDialog();
            iEnumerableToObservable(BL.GetStationList());
        }
    }
}
