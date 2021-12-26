using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public static class ToStringObjects
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (var item in t.GetType().GetProperties())
            {
                str += "\n" + item.Name
                + ": " + item.GetValue(t, null);
            }
            return str;
        }
    }
}
