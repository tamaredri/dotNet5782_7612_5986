using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationToList
    {

        public int ID { set; get; }
        public string Name { set; get; }
        public int AvailableChargeSlots { set; get; }
        public int UsedChargeSlots { set; get; }



        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
