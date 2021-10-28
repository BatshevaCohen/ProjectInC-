using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {

            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public Enums.WeightCategories Weight { get; set; }

            public Enums.Priorities Priority { get; set; }
            
            public  int DroneID { get; set; }

            public DateTime Requested { get; set; }//Time to create a package for the sender
            public DateTime Scheduled { get; set; }//Time to assign the package to the glider
            public DateTime PickedUp { get; set; }//Time to pick up the package from the sender
            public DateTime Delivered { get; set; }//Time of arrival of the package to the recipient
            public override string ToString()
            {
                String result = "";
                result += $"ID is {Id}, \n";
                result += $"Sending customer is {SenderId}, \n";
                result += $"Reciving customer is {TargetId}, \n";
                result += $"Package weight is {Weight}, \n";
                result += $"Priority is {Priority}, \n";
                result += $"Quadocopter ID is {DroneID}, \n";
                result += $"Creating package time is {Requested}, \n";
                result += $"Package to quadocopter affiliation time is {Scheduled}, \n";
                result += $"Collecting package time is {PickedUp}, \n";
                result += $"Client reciving time is {Delivered}, \n";

                return result;
            }

        }

    }
}
