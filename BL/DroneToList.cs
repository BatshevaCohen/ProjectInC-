using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public int Id { get; set; }
        public string model { get; set; }
        public string Model { get; internal set; }
        public Weight weight { get; set; }
        public double battery { get; set; }
        public DroneStatuses droneStatuses { get; set; }
        public Location location { get; set; }
        public int ParcelNumberTransferred { get; set; }//Parcel number transferred If there is 
    }
}
