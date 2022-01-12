using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL;
using BlApi;
using BO;
using System.ComponentModel;

namespace PO
{
    public class StationPO: INotifyPropertyChanged
    {
        private int id;
        public int ID
        {
            get
            { return id; }
            set
            {
                id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }

        private string name;
        public string Name
        {
            get
            { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        private Location stationLocation;
        public Location StationLocation
        {
            get
            { return stationLocation; }
            set
            {
                stationLocation = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StationLocation"));
                }
            }
        }

        private int availableChargeSlots;
        public int AvailableChargeSlots
        {
            get
            { return availableChargeSlots; }
            set
            {
                availableChargeSlots = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AvailableChargeSlots"));
                }
            }
        }

        private List<DroneInCharge> chargedDrones;
        public List<DroneInCharge> ChargedDrones
        {
            get
            { return chargedDrones; }
            set
            {
                chargedDrones = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ChargedDrones"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
