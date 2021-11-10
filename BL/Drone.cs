using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Drone
    {
        public int Id { get; set; }
        public string model { get; set; }
        public Weight weight { get; set; }
        public double Battery { get; set; } //charging level
        public DroneStatuses droneStatuses { get; set; }
        public ParcelInTransfer ParcelInTransfer { get; set; }
        public Location location { get; set; }
    }
}
