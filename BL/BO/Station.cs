using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public Location StationLocation { set; get; }
        public int AvailableChargeSlots { set; get; }
        public List<DroneInCharge> ChargedDrones { set; get; }

        public override string ToString()
        {
            int i = 1;
            string s = "ID: " + ID + "\nName: " + Name
                + "\n" + StationLocation.ToString() + "\nAvailable charge slots: " + AvailableChargeSlots + "\nchargedDrones: ";
            foreach (var item in ChargedDrones)
            {
                s += "\n" + i + ": " + item.ToStringProperty();
                i++;
            }
            return s;
        }

    }
}
