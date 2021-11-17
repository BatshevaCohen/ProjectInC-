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
        public string Model { get; set; }
        public Weight Weight { get; set; }
        public double Battery { get; set; } //charging level
        public DroneStatuses DroneStatuses { get; set; }
        public ParcelInTransfer ParcelInTransfer { get; set; }
        public Location Location { get; set; }
    }
}
