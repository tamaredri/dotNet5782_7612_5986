using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Station
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public double Longitude { set; get; }
        public double Lattitude { set; get; }
        public int ChargeSlots { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
