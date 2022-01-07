﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PLConverter
{
    public class NotVisibilityToVisibilityConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityValue = (Visibility)value;
            if (visibilityValue==Visibility.Hidden)
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
                return Visibility.Hidden; //Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
    }
}
