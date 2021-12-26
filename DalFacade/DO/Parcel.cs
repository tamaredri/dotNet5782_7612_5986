using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Parcel
    {
        public int ID { set; get; }
        public int SenderID { set; get; }
        public int TargetID { set; get; }
        public WeightCategories Weight { set; get; }
        public int DroneID { set; get; }
        public Prioritie Priority { set; get; }
        public DateTime? Requested { set; get; }
        public DateTime? Scheduled { set; get; }
        public DateTime? PickedUp { set; get; }
        public DateTime? Delivered { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
