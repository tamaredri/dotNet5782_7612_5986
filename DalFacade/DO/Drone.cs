using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public struct Drone
    {
        public int ID { set; get; }
        public string Model { set; get; }
        public WeightCategories MaxWeight { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
