﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneToList
    {
       
        public int ID { set; get; }
        public string Model { set; get; }
        public WeightCategories Weight { set; get; }
        public int Battery { set; get; }
        public DroneStatuses Status { set; get; }
        public Location DroneLocation { set; get; }
        public int ParcelId { set; get; }


        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
