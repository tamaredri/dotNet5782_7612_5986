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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddParcel.xaml
    /// </summary>
    public partial class AddParcel : Window
    {
        IBL BL;
        Parcel parcelToAdd = new Parcel();
        public AddParcel(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            DataContext = parcelToAdd;

            WeightSelectorComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            PrioritySelectorComboBx.ItemsSource = Enum.GetValues(typeof(Priorities));
        }

        #region header panel
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        #endregion

        #region customers combobox initializetion
        private void Target_DropDownOpen(object sender, EventArgs e)
        {
            if(SenderComboBox.SelectedItem is null)
            {
                TargetComboBox.ItemsSource = BL.GetCustomerList();
            }
            else 
            {
                TargetComboBox.ItemsSource = BL.GetPartOfCustomer(x=> x.Name != (SenderComboBox.SelectedItem as CustomerToList).Name);
            }
        }

        private void Sender_DropDownOpen(object sender, EventArgs e)
        {
            if (TargetComboBox.SelectedItem is null)
            {
                SenderComboBox.ItemsSource = BL.GetCustomerList();
            }
            else
            {
                SenderComboBox.ItemsSource = BL.GetPartOfCustomer(x => x.Name != (TargetComboBox.SelectedItem as CustomerToList).Name);
            }
        }
        #endregion

        #region creaate the parcel
        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parcelToAdd.Sender = new CustomerForParcel()
                {
                    ID = ((CustomerToList)SenderComboBox.SelectedItem).ID,
                    Name = ((CustomerToList)SenderComboBox.SelectedItem).Name
                };
                parcelToAdd.Target = new CustomerForParcel()
                {
                    ID = ((CustomerToList)TargetComboBox.SelectedItem).ID,
                    Name = ((CustomerToList)TargetComboBox.SelectedItem).Name
                };
                BL.CreateParcel(parcelToAdd);
                this.Close();
            }
            catch (Exception x) 
            {
                MessageBox.Show(x.Message,"EXCEPTION", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region sender / target selection changed
        private void Sender_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            AddButton.IsEnabled = !(SenderComboBox.SelectedItem is null || TargetComboBox.SelectedItem is null);

        private void Target_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            AddButton.IsEnabled = !(SenderComboBox.SelectedItem is null || TargetComboBox.SelectedItem is null);
        #endregion
    }
}
