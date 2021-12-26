using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public partial class BL
    {
        //------------------location------------------

        #region copyLocation
        /// <summary>
        /// deep copy of the location requested
        /// </summary>
        /// <remarks>include a validation of the location recieved</remarks>
        /// <param name="locToCopy">locatio to copy</param>
        /// <returns>returns a copy of the location</returns>
        Location copyLocation(Location locToCopy)
        {
            locToCopy.checkLongitudeLatitude();
            return new Location()
            {
                Lattitude = locToCopy.Lattitude,
                Longitude = locToCopy.Longitude
            };
        }
        #endregion

    }
}
