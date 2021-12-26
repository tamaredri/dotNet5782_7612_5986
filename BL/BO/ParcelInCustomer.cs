using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInCustomer
    {
        public int ID { set; get; }
        public WeightCategories Weight { set; get; }
        public Priorities Priority { set; get; }
        public ParcelStatuse Status { set; get; }
        public CustomerForParcel EndCustomer { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
