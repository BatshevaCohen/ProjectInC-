﻿using System;
namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int StationId { get; set; }
            public int DroneId { get; set; }




            public override string ToString()
            {
                String result = "";
                result += $"ID is {StationId}, \n";
                result += $"Name is {DroneId}, \n";
                return result;
            }

            public DroneCharge Find(Func<object, bool> p)
            {
                throw new NotImplementedException();
            }
        }
    }
}