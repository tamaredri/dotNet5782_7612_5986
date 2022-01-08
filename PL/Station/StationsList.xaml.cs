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

            foreach (var item in BL.GetStationList())
            {
                stationsList.Add(item);
            }

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
            IEnumerable<IGrouping<int, StationToList>> groupings = groupByAvailableChargeSlots(stationsList);
            groupings = groupings.OrderBy(g => g.Key);
            foreach (var group in groupings)
                TakenChargeSlotsComboBox.Items.Add(group.Key);
        }

        /// <summary>
        /// initialize the taken comboBox with the values according to the list of stations
        /// </summary>
        private void AvailableItems_DropDownOpened(object sender, EventArgs e)
        {
            TakenChargeSlotsComboBox.Items.Clear();
            TakenChargeSlotsComboBox.Items.Add("");
            IEnumerable<IGrouping<int, StationToList>> groupings = groupByAvailableChargeSlots(stationsList);
            groupings = groupings.OrderBy(g => g.Key);
            foreach (var group in groupings)
                TakenChargeSlotsComboBox.Items.Add(group.Key);
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
    }
}
