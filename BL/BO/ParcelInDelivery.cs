using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInDelivery
    {
        public int ID { set; get; }
        public bool InDelivery { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public CustomerForParcel Sender { set; get; }
        public CustomerForParcel Target { set; get; }
        public Location Destination { set; get; }
        public Location PickUp { set; get; }
        public double Distance { set; get; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
