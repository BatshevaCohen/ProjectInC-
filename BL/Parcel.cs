using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { get; init; }
        public CustomerInParcel Sender { get; set; } //the sender of the parcel
        public CustomerInParcel Reciver { get; set; } //the person who recive the parcel
        public Weight Weight { get; set; } //wheight category
        public Priority Priority { get; set; } // parcel's priority
        public DroneInParcel DroneInParcel { get; set; }
        public DateTime ParcelCreationTime { get; set; } // the time of the parcel's creation
        public DateTime AssignmentToParcelTime { get; set; } // the time when the parcel have assigned
        public DateTime CollectionTime { get; set; } // percel's collection time
        public DateTime SupplyTime { get; set; } //parcel's supply time
    }
}
