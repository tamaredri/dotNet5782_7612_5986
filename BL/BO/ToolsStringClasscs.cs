using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    

    public static class ToolStringClass
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
