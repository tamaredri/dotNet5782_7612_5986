using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharge
    {

        public int ID { set; get; }
        public int Battery { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
