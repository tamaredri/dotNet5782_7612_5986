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
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronesListView.xaml
    /// </summary>
    public partial class DronesListView : Window
    {
        readonly IBL BLAccess = BlFactory.GetBl();
        public DronesListView()
        {
            InitializeComponent();
            //droneDetails.DataContext = BLAccess.GetDrone(1);
            droneTemplate.DataContext = BLAccess.GetDrone(1);
        }
    }
}
