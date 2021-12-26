using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        
        public int ID { set; get; }
        public string Name { set; get; }
        public int Phone { set; get; }
        public int DeliveredParcels { set; get; }
        public int SentParcels { set; get; }
        public int ReceivedParcels { set; get; }
        public int OnTheWay { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
