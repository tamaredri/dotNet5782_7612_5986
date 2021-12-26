using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerForParcel
    {
        
        public int ID { set; get; }
       
        public string Name { set; get; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
