using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class ParcelInTransfer
    {
        public int Id { get; set; }
        public ParcelStatusBool parcelStatusBool { get; set; }
        public Priority priority { get; set; }
        public Weight weight { get; set; }
        public CustomerInParcel sender { get; set; }
        public CustomerInParcel reciver { get; set; }
        public Location collecting { get; set; }
        public Location SupplyTarget { get; set; }
        public Double TransportDistance { get; set; }
    }
}
