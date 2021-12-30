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
    /// Interaction logic for ParcelListView.xaml
    /// </summary>
    public partial class ParcelListView : Page
    {
        public ParcelListView(List<ParcelToList> parcelsList)
        {
            InitializeComponent();

            list.ItemsSource = parcelsList;
            list.DataContext = parcelsList;

            status.ItemsSource = Enum.GetValues(typeof(ParcelStatuse));
            priority.ItemsSource = Enum.GetValues(typeof(Priorities));
            weight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            //sender.ItemsSource = Enum.GetValues(typeof(?));
            //target.ItemsSource = Enum.GetValues(typeof(?));
        }
    }
}
