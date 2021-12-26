using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneForParcel
    {
        public int ID { set; get; }
        public int Battery { set; get; }
        public Location DroneToParcelLocation { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
