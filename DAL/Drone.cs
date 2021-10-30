using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int ID { get; set; }
            public int Model { get; set; }
            public Enums.Weight QWeight { get; set; }
            public int Battery { get; set; } //charging level
            public Enums.DroneState State { get; set; }
            
            public override string ToString()
            {
                String result = "";
                result += $"ID is {ID}, \n";
                result += $"Model is {Model}, \n";
                result += $"Quadocopster weight is {QWeight}, \n";
                result += $"Battery precent is {Battery}, \n";
                result += $"Quadocopster state is {State}, \n";

                return result;
            }

        }
    }
}