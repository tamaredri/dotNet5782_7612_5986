using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {

        public int ID { set; get; }
        public CustomerForParcel Sender { set; get; }
        public CustomerForParcel Target { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities ParcelPriorities { set; get; }
        public DateTime? CreateTime { set; get; }
        public DateTime? ScheduleTime { set; get; }
        public DateTime? PickUpTime { set; get; }
        public DateTime? DelivereTime { set; get; }
        public DroneForParcel DroneToDeliverParcel { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
