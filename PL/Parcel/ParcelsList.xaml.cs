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
using System.ComponentModel;

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
            IEnumerableToObservable(BL.GetParcelList());
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

        #region Selection changed - combobox
        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusComboBox.SelectedItem is ParcelStatuse)
                IEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Status == (ParcelStatuse)StatusComboBox.SelectedItem));
            else if (StatusComboBox.SelectedItem is "")
                IEnumerableToObservable(BL.GetParcelList());
            WeightComboBox.SelectedItem = "";
            PriorityComboBox.SelectedItem = "";
        }

        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriorityComboBox.SelectedItem is Priorities)
                IEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Priority == (Priorities)PriorityComboBox.SelectedItem));
            else if (PriorityComboBox.SelectedItem is "")
                IEnumerableToObservable(BL.GetParcelList());
            WeightComboBox.SelectedItem = "";
            StatusComboBox.SelectedItem = "";
        }

        private void Weight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeightComboBox.SelectedItem is WeightCategories)
                IEnumerableToObservable(BL.GetPartOfParcel(parcel => parcel.Weight == (WeightCategories)WeightComboBox.SelectedItem));
            else if (WeightComboBox.SelectedItem is "")
                IEnumerableToObservable(BL.GetParcelList());
            PriorityComboBox.SelectedItem = "";
            StatusComboBox.SelectedItem = "";
        }
        #endregion  

        /// <summary>
        /// convert from ienumerable to an observable collection
        /// </summary>
        /// <param name="listTOConvert">IEnumerable to convert</param>
        private void IEnumerableToObservable(IEnumerable<ParcelToList> listTOConvert)
        {
            parcelsList.Clear();
            foreach (var station in listTOConvert)
                parcelsList.Add(station);
        }

        #region group by sender / target
        private void GroupBySender_Click(object sender, RoutedEventArgs e)
        {
            RemoveGroupings_Click(sender, e);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(PList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("SenderName");
            SortDescription sortDscription = new SortDescription("SenderName", ListSortDirection.Ascending);
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(sortDscription);
            GroupBySenderButton.IsEnabled = false;
        }

        private void GroupByTarget_Click(object sender, RoutedEventArgs e)
        {
            RemoveGroupings_Click(sender, e);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(PList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("TargetName");
            SortDescription sortDscription = new SortDescription("TargetName", ListSortDirection.Ascending);
            view.GroupDescriptions.Add(groupDescription);
            view.SortDescriptions.Add(sortDscription);
            GroupByTargetButton.IsEnabled = false;
        }

        private void RemoveGroupings_Click(object sender, RoutedEventArgs e)
        {
            PList.ItemsSource = BL.GetParcelList();
        }
        #endregion

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            AddParcel add = new AddParcel(BL);
            add.ShowDialog();
            IEnumerableToObservable(BL.GetParcelList());
        }

        private void OpenParcel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParcelSingleView parcelSingleView = new ParcelSingleView(BL, ((sender as DataGrid).SelectedItem as ParcelToList).ID);
            parcelSingleView.ShowDialog();
            IEnumerableToObservable(BL.GetParcelList());
        }
    }
}
