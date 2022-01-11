﻿using BO;
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
                return  false; //Visibility.Collapsed;
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString());
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
}
