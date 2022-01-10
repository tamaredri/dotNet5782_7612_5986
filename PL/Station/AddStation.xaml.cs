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

        #region block values in textbox
        /// <summary>
        /// block the text box only to digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)

        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;



            //allow get out of the text box

            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)

                return;



            //allow list of system keys (add other key here if you want to allow)

            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||

                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||

                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)

                return;



            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            //allow control system keys

            if (Char.IsControl(c)) return;



            //allow digits (without Shift or Alt)

            if (Char.IsDigit(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return; //let this key be written inside the textbox



            //forbid letters and signs (#,$, %, ...)

            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls

            return;

        }

        /// <summary>
        /// block the text bx onky to letters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text == null) return;

            if (e == null) return;



            //allow get out of the text box

            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)

                return;



            //allow list of system keys (add other key here if you want to allow)

            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||

                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home || e.Key == Key.End ||

                e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)

                return;



            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);



            //allow control system keys

            if (Char.IsControl(c)) return;



            //allow digits (without Shift or Alt)

            if (Char.IsLetter(c))

                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))

                    return; //let this key be written inside the textbox



            //forbid letters and signs (#,$, %, ...)

            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls

            return;
        }
        #endregion

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

        private void AvailableChargeSlots_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = sender as TextBox;
            text.Clear();
        }
    }
}
