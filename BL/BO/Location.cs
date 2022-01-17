using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Math.Sqrt

namespace BO
{
    public class Location
    {
        public double Lattitude { set; get; }
        public double Longitude { set; get; }
        public override string ToString()
        {
            double _latitude = Lattitude;
            double _longitude = Longitude;
            string lat()
            {
                string ch = "N";
                if (_latitude < 0)
                {
                    ch = "S";
                    _latitude = -_latitude;
                }
                int deg = (int)_latitude;
                int min = (int)(60 * (_latitude - deg));
                float sec = (float)(_latitude - deg) * 3600 - min * 60;

                return $"{deg}° {min}' {sec}'' {ch}";
            }

            string log()
            {
                string ch = "E";
                if (_longitude < 0)
                {
                    ch = "W";
                    _longitude = -_longitude;
                }
                int deg = (int)_longitude;
                int min = (int)(60 * (_longitude - deg));
                float sec = (float)(_longitude - deg) * 3600 - min * 60;

                return $"{deg}° {min}' {sec}'' {ch}";
            }

            return log() + "+" + lat();
        }
    }
}
