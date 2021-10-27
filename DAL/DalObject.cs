using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DO;
using IDAL;
using IDAL.DO;

namespace DalObject
{
    public class DalObject
    {
        //add functions
        public int AddBaseStation(BaseStation baseStation)
        {
            DataSource.baseStations.Add(baseStation);
            return 1;
        }
        public int AddDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
            return 1;
        }
        public int AddCustomer(Customer customer)
        {
            DataSource.customer.Add(customer);
            return 1;
        }
        public int AddPackage(Package package)
        {
            DataSource.packages.Add(package);
            return 1;
        }

        //update functions
        public void UpdatePackageToDrone()
        {

        }
        public void UpdatePackageCollectionByDrone()
        {

        }
        public void UpdateSupplyPackageToCustomer()
        {

        }
        public void UpdateDroneToCharge()
        {

        }
        public void DischargeDrone()
        {

        }

        //view function
        public void ShowBaseStation(int id)
        {

        }
        public void ShowDrone(int id)
        {

        }
        public void ShowCustomer(int id)
        {

        }
        public void ShowPackage(int id)
        {

        }

        //view lists functions
        public void ShowBaseStationList()
        {

        }
        public void ShowDroneList()
        {

        }
        public void ShowCustomerList()
        {

        }
        public void ShowPackageList()
        {

        }
        // shows the list of packages that haven't been associated to a drone
        public void ShowNonAssociatedPackageList()
        {

        }
        // shows base stations with available charging spots
        public void ShowChargeableBaseStationList()
        {

        }
        //EXIT
        public void Exit()
        {

        }
    }
}
