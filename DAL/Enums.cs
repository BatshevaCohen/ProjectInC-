using System;
namespace IDAL
{
    namespace DO
    {
        public struct Enums
        {
            public enum WeightCategories
            {
                Light,
                Medium,
                Heavy
            }

            public enum DroneStatuses
            {
                Available,
                Maintenance,
                Shipping
            }

            public enum Priorities
            {
                Regular,
                Fast,
                Emergency
            }
        }
    }
}