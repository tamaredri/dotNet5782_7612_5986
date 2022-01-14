using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BlApi;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace PLConverter
{
    public class NotVisibilityToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityValue = (Visibility)value;
            if (visibilityValue == Visibility.Hidden)
            {
                return Visibility.Visible; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }


        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityValue = (Visibility)value;
            if (visibilityValue == Visibility.Hidden)
            {
                return Visibility.Visible; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
    }
    public class BoolToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return Visibility.Visible; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Hidden;
            }
        }


        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityValue = (Visibility)value;
            if (visibilityValue is Visibility.Visible)
            {
                return true; //Visibility.Collapsed;
            }
            else
            {
                return false;
            }
        }
    }
    public class FalseToTrueConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return false; //Visibility.Collapsed;
            }
            else
            {
                return true;
            }
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
            {
                return false; //Visibility.Collapsed;
            }
            else
            {
                return true;
            }
        }
    }
    public class FalseToTrueConverterDataGrid : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue && parameter is DataGrid && !(parameter as DataGrid).IsGrouping)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue && parameter is DataGrid && !(parameter as DataGrid).IsGrouping)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class IntToStringPhoneConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "0" + value;
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => int.Parse(value.ToString());
    }

    public class IntToStringConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //try { int.Parse(value.ToString()); }
            if (value is not default(string)) { return int.Parse(value.ToString()); }

            else return null;
        }
    }

    public class StringConverterTodouble : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //try { int.Parse(value.ToString()); }
            return value.ToString();
        }
    }

    public class StringConverterToIntBattery : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.Parse(value.ToString()) > 100)
                return 100;
            else return int.Parse(value.ToString());
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value.ToString();
        }
    }
    public class TextToBool : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "")
                return false;
            else return true;

        }



        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }

    public class FalseToTrueUpdateConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.ToString() == "" && parameter.ToString() == "") 
            {
                return false; //Visibility.Collapsed;
            }
            else
            {
                return true;
            }
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return "";
        }
    }



    #region drne options

    #region SentToCharge or schedule
    public class SentToChargeOrSchedule : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.available)
                return true;
            else return false;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.available;
        }


    }
    public class SentToChargeOrScheduleVisability : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.available)
                return Visibility.Visible;
            else return Visibility.Hidden;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.available;
        }


    }
    #endregion
    #region ReleseFromCharge
    public class ReleseFromChargeConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.maintenance)
                return true;
            else return false;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.maintenance;
        }


    }
    public class ReleseFromChargeVisabilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.maintenance)
                return Visibility.Visible;
            else return Visibility.Hidden;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.maintenance;
        }


    }
    #endregion
    #region Delivery
    public class DeliveryConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ParcelInDelivery)value != null && (value as ParcelInDelivery).InDelivery)
                return true;
            else return false;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }
    public class DeliveryVisivilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ParcelInDelivery)value != null && (value as ParcelInDelivery).InDelivery)
                return Visibility.Visible;
            else return Visibility.Hidden;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }
    #endregion
    #region Pickup
    public class PickupConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ParcelInDelivery)value != null && !(value as ParcelInDelivery).InDelivery)
                return true;
            else return false;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }

    public class PickupVisivilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ParcelInDelivery)value != null && !(value as ParcelInDelivery).InDelivery)
                return Visibility.Visible;
            else return Visibility.Hidden;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }
    #endregion
    #region iS Delivery
    public class IsDeliveryConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.delivery)
                return true;
            else return false;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }
    public class IsDeliveryVisabilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((DroneStatuses)value == DroneStatuses.delivery)
                return Visibility.Visible;
            else return Visibility.Hidden;

        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //never used
            return DroneStatuses.delivery;
        }


    }
    #endregion


    #endregion

    //public class StatusToInt : IValueConverter
    //{
    //    //convert from source property type to target property type
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return (int)value;
    //    }



    //    //convert from target property type to source property type
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        //never used
    //        return DroneStatuses.available;
    //    }
    //}

    public class CanDeleteParcelStatusConverter : IValueConverter
    {

        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ( (value is ParcelStatuse) && (ParcelStatuse)value is ParcelStatuse.created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((value is Button) && (value as Button).IsEnabled is true)
            {
                return ParcelStatuse.created;
            }
            else
            {
                return ParcelStatuse.delivered;
            }
        }
    }
}





//public class StringToWeight : IValueConverter
//{
//    //convert from source property type to target property type
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        return value.ToString();
//    }



//    //convert from target property type to source property type
//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        return (WeightCategories)((int)value);
//    }
//}

//public class NameToLocation : IValueConverter
//{
//    //convert from source property type to target property type
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        //IBL BL = parameter as IBL;
//        //return BL.GetPartOfStation(x => x.ID == (int)value).FirstOrDefault().Name;
//        throw new Exception("converter never used");
//    }



//    //convert from target property type to source property type
//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        IBL BL = parameter as IBL;
//        Station station =BL.GetStation(BL.GetPartOfStation(x => x.Name == value.ToString()).FirstOrDefault().ID);
//        return new Location() { Lattitude = station.StationLocation.Lattitude, Longitude = station.StationLocation.Longitude };
//    }
//}

