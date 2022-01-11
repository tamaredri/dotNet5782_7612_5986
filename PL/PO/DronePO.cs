using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;

namespace PL
{
    public class DronePO: INotifyPropertyChanged
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

        private string model;
        public string Model
        {
            get
            { return model; }
            set
            {
                model = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                }
            }
        }

        private WeightCategories weight;
        public WeightCategories Weight
        {
            get
            { return weight; }
            set
            {
                weight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
                }
            }
        }

        private int battery;
        public int Battery
        {
            get
            { return battery; }
            set
            {
                battery = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
                }
            }
        }

        private DroneStatuses status;
        public DroneStatuses Status
        {
            get
            { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        private Location location;
        public Location Location
        {
            get
            { return location; }
            set
            {
                location = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }
        }

        private ParcelInDelivery parcelInDeliveryByDrone;
        public ParcelInDelivery ParcelInDeliveryByDrone
        {
            get
            { return parcelInDeliveryByDrone; }
            set
            {
                parcelInDeliveryByDrone = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelInDeliveryByDrone"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
