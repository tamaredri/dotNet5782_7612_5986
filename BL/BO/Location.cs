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
            return this.ToStringProperty();
        }
    }
}
