using System;

namespace IDAL
{
    namespace DO
    {
        public struct Package
        {

            public int ID { get; set; }
            public int SendingCustomer { get; set; }
            public int GettingCustomer { get; set; }
            public Enums.Weight PWeight { get; set; }

            public Enums.Priority Priority { get; set; }
            public  int QuadocopterID { get; set; }

            public DateTime Create { get; set; }//Time to create a package for the sender
            public DateTime Affiliation { get; set; }//Time to assign the package to the glider
            public DateTime Collection { get; set; }//Time to pick up the package from the sender
            public DateTime Supply { get; set; }//Time of arrival of the package to the recipient
            public override string ToString()
            {
                String result = "";
                result += $"ID is {ID}, \n";
                result += $"Sending customer is {SendingCustomer}, \n";
                result += $"Reciving customer is {GettingCustomer}, \n";
                result += $"Package weight is {PWeight}, \n";
                result += $"Priority is {Priority}, \n";
                result += $"Quadocopter ID is {QuadocopterID}, \n";
                result += $"Creating package time is {Create}, \n";
                result += $"Package to quadocopter affiliation time is {Affiliation}, \n";
                result += $"Collecting package time is {Collection}, \n";
                result += $"Client reciving time is {Supply}, \n";

                return result;
            }

        }

    }
}
