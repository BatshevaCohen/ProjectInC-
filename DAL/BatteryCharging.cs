using System;
namespace IDAL
{
    namespace DO
    {
        public struct BatteryCharging
        {
            public int IDBaseStation { get; set; }
            public int IDQuadocopter { get; set; }




            public override string ToString()
            {
                String result = "";
                result += $"ID is {IDBaseStation}, \n";
                result += $"Name is {IDQuadocopter}, \n";
                return result;
            }

        }
    }
}