using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public Enums.WeightCategories MaxWeight { get; set; }
            public double Battery { get; set; } //charging level
            public Enums.DroneStatuses Staus { get; set; }
            
            public override string ToString()
            {
                String result = "";
                result += $"ID is {Id}, \n";
                result += $"Model is {Model}, \n";
                result += $"Drone weight is {MaxWeight}, \n";
                result += $"Battery precent is {Battery}, \n";
                result += $"Drone state is {Staus}, \n";

                return result;
            }

        }
    }
}