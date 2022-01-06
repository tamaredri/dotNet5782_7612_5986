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
    /// Interaction logic for entryPage.xaml
    /// </summary>
    public partial class entryPage : Window
    {
        IBL BLAccess;
        public entryPage()
        {
            BLAccess = BlFactory.GetBl();
            InitializeComponent();
        }

    }
}
