using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct DroneCharge
    {
        public int Droneld { set; get; }
        public int Stationld { set; get; }
        public bool IsInCharge { set; get; }
        public DateTime? TimeStart { set; get; }
        public DateTime? TimeFinish { set; get; }
        public int ID { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
