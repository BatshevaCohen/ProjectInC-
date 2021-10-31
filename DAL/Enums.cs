using System;
namespace IDAL
{
    namespace DO
    {
        public struct Enums
        {
            public enum WeightCategories
            {
                Light = 1,
                Medium,
                Heavy
            }

            public enum DroneStatuses
            {
                Available = 1 ,
                Maintenance,
                Shipping
            }

            public enum Priorities
            {
                Regular = 1,
                Fast,
                Emergency
            }
        }
    }
}