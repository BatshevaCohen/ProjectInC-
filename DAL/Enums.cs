using System;
namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories
        {
            Light = 1,
            Medium,
            Heavy
        }

        public enum DroneStatuses
        {
            Available = 1,
            Maintenance,
            Shipping
        }

        public enum Priorities
        {
            Standart = 1,
            Fast,
            Emergency
        }
        public enum Severity
        {
            Mild = 1,
            Severe,
            Terrible
        }
    }
}
