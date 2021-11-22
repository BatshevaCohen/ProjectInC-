using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int AvailableChargingSpots { get; set; } //Number of available charging spots in free

        public List<DroneInCharging> droneInChargings= new List<DroneInCharging>(); //list of drones in charging

    }
}
