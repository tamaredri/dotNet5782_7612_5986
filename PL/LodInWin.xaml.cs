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
    /// Interaction logic for LodInWin.xaml
    /// </summary>
    public partial class LogInWin : Window
    {
        public LogInWin()
        {
            InitializeComponent();
        }

        private void ManagerLogIn_Click(object sender, RoutedEventArgs e)
        {
            managerButton.Visibility = Visibility.Hidden;
            managerButton.IsEnabled = false;

        }

        private void CloseManagerLogIn_Click(object sender, RoutedEventArgs e)
        {
            managerButton.Visibility = Visibility.Visible;
            managerButton.IsEnabled = true;
        }

        private void PanelHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ManagerlogInWithPassword_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "1234")
            {
                HomePageManager homeManager = new();
                homeManager.ShowDialog();
            }
                
        }
    }
}
