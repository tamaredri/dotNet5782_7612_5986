using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Customer
    {
       
        public int ID { set; get; }
        public string Name { set; get; }
        public int Phone { set; get; }
        public Location LocationOfCustomer { set; get; }
        public List<ParcelInCustomer> Sent { set; get; }
        public List<ParcelInCustomer> Recieved { set; get; }

        public override string ToString()
        {
            int i = 1;
            string s = "ID: " + ID + "\nName: " + Name
                + "\nPhone: 0" + Phone + "\n" + LocationOfCustomer.ToString() + "\nSent:";
            foreach (var item in Sent)
            {
                s += "\n" + i + ": " + item.ToString();
                i++;
            }
            i = 1;
            s += "\nRecieved:";
            foreach (var item in Recieved)
            {
                s += "\n" + +i + ": " + item.ToString();
                i++;
            }
            return s;

        }

    }
}
