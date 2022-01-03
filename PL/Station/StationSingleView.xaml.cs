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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationSingleView.xaml
    /// </summary>
    public partial class StationSingleView : Page
    {
        IBL BLAccess;
        public StationSingleView(IBL BLAccess, StationToList station)
        {
            InitializeComponent();

            this.BLAccess = BLAccess;
            StationDetails.DataContext = BLAccess.GetStation(station.ID);
        }
    }
}
