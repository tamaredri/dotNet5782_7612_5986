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
    /// Interaction logic for CustometSingleView.xaml
    /// </summary>
    public partial class CustometSingleView : Page
    {
        IBL BLAccess;
        public CustometSingleView(IBL BLAccess, int IdcustomerToShow)
        {
            InitializeComponent();
            this.BLAccess = BLAccess;
            parcelInTheWay.ItemsSource = BLAccess.GetCustomer(IdcustomerToShow).Sent;
            sentParcel.ItemsSource = BLAccess.GetCustomer(IdcustomerToShow).Recieved;
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               