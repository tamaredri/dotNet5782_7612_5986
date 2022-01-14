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

namespace PL
{
    /// <summary>
    /// Interaction logic for LocationWin.xaml
    /// </summary>
    public partial class LocationWin : Window
    {
        public LocationWin()
        {
            InitializeComponent();
            
            googleMap.Source = new Uri("https://www.google.com/maps/@32.3382113,33.2820225,7.35z?hl=iw");
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e) => this.Close();

        private void googleMap_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("hii");
        }

        private void googleMap_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("hii");

        }

        private void googleMap_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("hii");
        }

        private void googleMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("hii");

        }
    }

}
