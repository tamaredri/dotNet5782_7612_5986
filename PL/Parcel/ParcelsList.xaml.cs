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
    /// Interaction logic for ParcelsList.xaml
    /// </summary>
    public partial class ParcelsList : Page
    {
        IBL BL;
        ObservableCollection<ParcelToList> parcelsList = new();

        public ParcelsList(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            iEnumerableToObservable(BL.GetParcelList());
            DataContext = parcelsList;
        }

        #region comboBox initializetion
        /// <summary>
        /// initialize the status comboBox with the values according to the list of parcels
        /// </summary>
        private void Status_DropDownOpened(object sender, EventArgs e)
        {
            StatusComboBox.Items.Clear();
            StatusComboBox.Items.Add("");
            IEnumerable<IGrouping<ParcelStatuse, ParcelToList>> groupings = groupByStatus(BL.GetParcelList());
            groupings = groupings.OrderBy(p => p.Key);
            foreach (var group in groupings)
                StatusComboBox.Items.Add(group.Key);
        }

        /// <summary>
        /// initialize the priority comboBox with the values according to the list of parcels
        /// </summary>
        private void Priority_DropDownOpened(object sender, EventArgs e)
        {
            PriorityComboBox.Items.Clear();
            PriorityComboBox.Items.Add("");
            IEnumerable<IGrouping<Priorities, ParcelToList>> groupings = groupByPriority(BL.GetParcelList());
            groupings = groupings.OrderBy(p => p.Key);
            foreach (var group in groupings)
                PriorityComboBox.Items.Add(group.Key);
        }

        /// <summary>
        /// initialize the priority comboBox with the values according to the list of parcels
        /// </summary>
        private void Weight_DropDownOpened(object sender, EventArgs e)
        {
            WeightComboBox.Items.Clear();
            WeightComboBox.Items.Add("");
            IEnumerable<IGrouping<WeightCategories, ParcelToList>> groupings = groupByWeight(BL.GetParcelList());
            groupings = groupings.OrderBy(p => p.Key);
            foreach (var group in groupings)
                WeightComboBox.Items.Add(group.Key);
        }
        #endregion

        #region group 
        /// <summary>
        /// group the list of parcels according to the status in eace parcel in the list
        /// </summary>
        /// <param name="listToGroup">the list to group by</param>
        /// <returns>IEnumerable with the groups</returns>
        IEnumerable<IGrouping<ParcelStatuse, ParcelToList>> groupByStatus(IEnumerable<ParcelToList> listToGroup)
       => (from parcel in listToGroup
           group parcel by parcel.Status into parcelInfo
           select parcelInfo).ToList();

        /// <summary>
        /// group the list of parcels according to the priority in eace parcel in the list
        /// </summary>
        /// <param name="listToGroup">the list to group by</param>
        /// <returns>IEnumerable with the groups</returns>
        IEnumerable<IGrouping<Priorities, ParcelToList>> groupByPriority(IEnumerable<ParcelToList> listToGroup)
       => (from parcel in listToGroup
           group parcel by parcel.Priority into parcelInfo
           select parcelInfo).ToList();

        /// <summary>
        /// group the list of parcels according to the weight in eace parcel in the list
        /// </summary>
        /// <param name="listToGroup">the list to group by</param>
        /// <returns>IEnumerable with the groups</returns>
        IEnumerable<IGrouping<WeightCategories, ParcelToList>> groupByWeight(IEnumerable<ParcelToList> listToGroup)
       => (from parcel in listToGroup
           group parcel by parcel.Weight into parcelInfo
           select parcelInfo).ToList();
        #endregion

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusComboBox.SelectedItem is ParcelStatuse)
                iEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Status == (ParcelStatuse)StatusComboBox.SelectedItem));
            else if (StatusComboBox.SelectedItem is "")
                iEnumerableToObservable(BL.GetParcelList());
            WeightComboBox.SelectedItem = "";
            PriorityComboBox.SelectedItem = "";
        }

        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriorityComboBox.SelectedItem is Priorities)
                iEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Priority == (Priorities)PriorityComboBox.SelectedItem));
            else if (PriorityComboBox.SelectedItem is "")
                iEnumerableToObservable(BL.GetParcelList());
            WeightComboBox.SelectedItem = "";
            StatusComboBox.SelectedItem = "";
        }

        private void Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightComboBox.SelectedItem is WeightCategories)
                iEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Weight == (WeightCategories)WeightComboBox.SelectedItem));
            else if (WeightComboBox.SelectedItem is "")
                iEnumerableToObservable(BL.GetParcelList());
            PriorityComboBox.SelectedItem = "";
            StatusComboBox.SelectedItem = "";
        }



        private void GroupBySender_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// convert from ienumerable to an observable collection
        /// </summary>
        /// <param name="listTOConvert">IEnumerable to convert</param>
        private void iEnumerableToObservable(IEnumerable<ParcelToList> listTOConvert)
        {
            parcelsList.Clear();
            foreach (var station in listTOConvert)
                parcelsList.Add(station);
        }
    }
}
