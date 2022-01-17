using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public static class CopyProperties
    {
        #region copy properties
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())//loop on all the properties in the new object
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);//check if there is property with the same name in the source object and get it
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);//get the value of the prperty
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);//insert the value to the suitable property
            }
        }
        #endregion
    }
}
