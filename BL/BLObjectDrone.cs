﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using DalObject.DO;
using IBL.BO;
using IDAL.DO;

namespace IBL.BO
{
    public partial class BLObject : IBL
    {
        /// <summary>
        /// Add drone
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int AddDrone(Drone drone, int stationId)
        {
            IDAL.DO.Drone d = new IDAL.DO.Drone();
            d.Id = drone.Id;
            d.Model = drone.Model;
            d.MaxWeight = (WeightCategories)drone.Weight;
            dalo.AddDrone(d);
            dalo.UpdateDroneToStation(stationId, d);

            throw new NotImplementedException();
        }
        /// <summary>
        /// Update drone's name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDroneName(int id, string model)
        {
            dalo.UpdateNameOfDrone(id, model);
        }
        /// <summary>
        /// Charging drone
        /// </summary>
        /// <param name="droneId"></param>
        public void UpdateChargeDrone(int droneId)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            //finds the drone by the recived ID
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            //if the drone is available- it can be sent for charging
            if (dronel.DroneStatuses == DroneStatuses.Available) 
            {
                List<Distanse> disStationFromDrone = dalo.MinimumDistance(dronel.Location.Longitude, dronel.Location.Latitude).ToList();//list of the distances from the drone to every station
                double min = 9999999;
                int idS, counter = 0;
                bool flag = false;
                //number of distances in the list
                int sized = disStationFromDrone.Count;
                //goes over the list
                while (!flag && counter <= sized) 
                {
                    foreach (Distanse item in disStationFromDrone)
                    {
                        //to find the station with the minimum distance from the drone
                        if (item.distance <= min) 
                        {
                            min = item.distance;
                            idS = item.id;
                        }
                        station = dalo.GetStation(item.id);
                        //if there is an available charging spot in the station
                        if (station.ChargeSlots > 0) 
                        {
                            //only if there is enough battery
                            if (dronel.Battery > min * 10 / 100) 
                            {
                                flag = true;
                                //function to update Battery, drone mode drone location
                                updateDroneToStation(droneId, station.Id, min);
                            }

                        }
                        counter++;
                        disStationFromDrone.Remove(item);
                    }
                }
            }
        }
        /// <summary>
        /// Update drone to station
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="stationId"></param>
        /// <param name="minDistance"></param>
        public void updateDroneToStation(int droneId, int stationId, double minDistance)
        {
            //update for the way to the station
            //finds the drone by its ID
            DroneToList dronel = drones.Find(x => x.Id == droneId);
            IDAL.DO.Station station = new IDAL.DO.Station();
            //finds the station by its ID
            station = dalo.GetStation(stationId);
            //update the drone to charging status
            dronel.DroneStatuses = DroneStatuses.Maintenance;
            //update the drone's location to the charging station location - latitude
            dronel.Location.Latitude = station.Latitude;
            //update the drone's location to the charging station location - longitudw
            dronel.Location.Longitude = station.Longitude; 
            double droneBattery = minDistance * 10 / 100;
            dronel.Battery = droneBattery;
            dalo.UpdateChargeSpots(station.Id);
            DroneInCharging droneInCharging = new DroneInCharging();
            droneInCharging.Battery = droneBattery;
            droneInCharging.Id = droneId;
            Station s = new Station();
            s.droneInChargings.Add(droneInCharging);
        }
        /// <summary>
        /// Discharge drone
        /// </summary>
        /// <param name="droneID"></param>
        /// <param name="chargingTime"></param>
        /// <exception cref="Exception"></exception>
        public void DischargeDrone(int droneID, TimeSpan chargingTime)
        {
            // save dVal in second
            double dVal = (chargingTime.TotalMilliseconds)*1000;
 
            //finds the drone by its ID
            DroneToList dronel = drones.Find(x => x.Id == droneID); 
            Station station = new Station();
            //only a drone that was in charging c
           
            //ould be discharge
            if (dronel.DroneStatuses == DroneStatuses.Maintenance) 
            {
                double droneLocationLatitude = dronel.Location.Latitude;
                double droneLocationLongitude = dronel.Location.Longitude;
                dalo.DischargeDrone(droneID, droneLocationLatitude, droneLocationLongitude);
                DroneInCharging droneInCharge = new DroneInCharging();
                //find the drone in charging
                droneInCharge = station.droneInChargings.Find(x => x.Id == droneID);
                //remove the drone frome the list of droneInChargings
                station.droneInChargings.Remove(droneInCharge); 
            }
            else
            {
                throw new Exception("drone can't be discharged");
            }
            dronel.Battery += dVal * dalo.PowerRequest()[4];
            dronel.DroneStatuses = DroneStatuses.Available;
        }
        /// <summary>
        /// Get drone by ID
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        BO.Drone GetDrone(int droneId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Show LIST of drones
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Drone> ShowDroneList()
        {
            throw new NotImplementedException();
        }
    }

}