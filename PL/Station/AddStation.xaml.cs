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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL BL;
        Station stationToCreate = new Station();
        Location location = new Location();
        public AddStation(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            DataContext = stationToCreate;
            LongitudeTextBox.DataContext = location;
            LattitudeTextBox.DataContext = location;

            #region clear controllers value
            AvailableChargeSlots.Clear();
            LongitudeTextBox.Clear();
            LattitudeTextBox.Clear();
            #endregion

            #region register to events
            NameTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyLetters_PreviewKeyDown;
            AvailableChargeSlots.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
            LongitudeTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
            LattitudeTextBox.PreviewKeyDown += BlockValuesClass.TextBox_OnlyNumbers_PreviewKeyDown;
            #endregion
        }

        #region panel header events
        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();
        #endregion

        private void AddStationToBL_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                stationToCreate.StationLocation = location;
                BL.CreateStation(stationToCreate);
                this.Close();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValueChanged_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddTheStation.IsEnabled = (NameTextBox.Text is not "" && AvailableChargeSlots.Text is not ""
                && LongitudeTextBox.Text is not "" && LattitudeTextBox.Text is not "");
        }
    }
}
