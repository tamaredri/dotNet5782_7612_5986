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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        IBL BL;
        Station stationToCreate = new Station();
        public AddStation(IBL BLAccess)
        {
            InitializeComponent();
            BL = BLAccess;
            DataContext = stationToCreate;
           // BL.CreateStation(new Station());
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        private void AddStationToBL_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                BL.CreateStation(stationToCreate);
                this.Close();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
