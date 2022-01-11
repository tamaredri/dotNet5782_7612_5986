using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class CustomerPO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                    PropertyChanged(this, new PropertyChangedEventArgs("Model"));
                }
            }
        }

        private int phone;
        public int Phone
        {
            get
            { return phone; }
            set
            {
                phone = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Weight"));
                }
            }
        }

        private Location locationOfCustomer;
        public Location LocationOfCustomer
        {
            get
            { return locationOfCustomer; }
            set
            {
                locationOfCustomer = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
                }
            }
        }

        private List<ParcelInCustomer> sent;
        public List<ParcelInCustomer> Sent
        {
            get
            { return sent; }
            set
            {
                sent = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelInDeliveryByDrone"));
                }
            }
        }
        
        private List<ParcelInCustomer> recieved;
        public List<ParcelInCustomer> Recieved
        {
            get
            { return recieved; }
            set
            {
                recieved = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ParcelInDeliveryByDrone"));
                }
            }
        }
    }
}
