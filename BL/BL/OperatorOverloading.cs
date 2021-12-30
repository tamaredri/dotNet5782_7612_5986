using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public static class OperatorOverloading
    {
        //public static bool Equles<T>(this T caller, T comparer)
        //{
        //    Type type = caller.GetType();
        //    if (type.Namespace == "BL")
        //    {
        //        return caller.
        //    }

        //}

        //public static bool Equles(this Customer caller, Customer comparer)
        //{
        //    return caller.ID == comparer.ID;
        //}

        //public static bool Equles(this Drone caller, Drone comparer)
        //{
        //    return caller.ID == comparer.ID;
        //}

        //public static bool Equles(this Parcel caller, Parcel comparer)
        //{
        //    return caller.ID == comparer.ID;
        //}

        //public static bool Equles(this Drone caller, Drone comparer)
        //{
        //    return caller.ID == comparer.ID;
        //}
        //public static bool operator == (this Station caller, Station comparer)
        //{
        //    return caller.Name == comparer.Name;
        //}

        //public static bool operator !=(this Station)
        //{

        //}
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
    }
}
