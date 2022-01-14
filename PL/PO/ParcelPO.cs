using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class ParcelPO : INotifyPropertyChanged
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

        private CustomerForParcel sender;
        public CustomerForParcel Sender
        {
            get
            { return sender; }
            set
            {
                sender = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
                }
            }
        }
        
        private CustomerForParcel target;
        public CustomerForParcel Target
        {
            get
            { return target; }
            set
            {
                target = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Sender"));
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

        private Priorities parcelPriorities;
        public Priorities ParcelPriorities
        {
            get
            { return parcelPriorities; }
            set
            {
                parcelPriorities = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Priority"));
                }
            }
        }

        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get
            { return createTime; }
            set
            {
                createTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CreateTime"));
                }
            }
        }

        private DateTime? scheduleTime;
        public DateTime? ScheduleTime
        {
            get
            { return scheduleTime; }
            set
            {
                scheduleTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ScheduleTime"));
                }
            }
        }
        private DateTime? pickUpTime;
        public DateTime? PickUpTime
        {
            get
            { return pickUpTime; }
            set
            {
                pickUpTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PickUpTime"));
                }
            }
        }

        private DateTime? delivereTime;
        public DateTime? DelivereTime
        {
            get
            { return delivereTime; }
            set
            {
                delivereTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DelivereTime"));
                }
            }
        }

        private DroneForParcel droneToDeliverParcel;
        public DroneForParcel DroneToDeliverParcel
        {
            get
            { return droneToDeliverParcel; }
            set
            {
                droneToDeliverParcel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("droneToDeliverParcel"));
                }
            }
        }

        private bool isPaired;
        public bool IsPaired
        {
            get { return isPaired; }
            set
            {
                isPaired = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsPaired"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
