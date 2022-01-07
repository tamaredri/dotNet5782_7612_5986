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
            LoginManager loginManager = new LoginManager();

        }
    }
}
