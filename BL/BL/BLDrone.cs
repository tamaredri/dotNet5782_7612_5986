using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using DO;

namespace BL
{
    public partial class BL
    {

        //------------------------Drone------------------------
        #region CreateDrone
        public void CreateDrone(BO.Drone droneToCreate, int idStationToCharge)
        {
            DO.Station stationToCharge;
            try
            { 
                stationToCharge = DalAccess.GetStation(idStationToCharge); 
            }
            catch (DO.DoesntExistExeption x)
            { 
                throw new BO.DoesntExistExeption("wrong station ID", x); 
            }

            DO.Drone doDronelToCreate = new DO.Drone()
            {
                Model = droneToCreate.Model,
                MaxWeight = (DO.WeightCategories)droneToCreate.MaxWeight
            };

            try
            {
                int DroneID = DalAccess.CreateDrone(doDronelToCreate);
                DalAccess.SendToCharge(DroneID, idStationToCharge);

                BO.DroneToList newDrone = new()
                {
                    ID = DroneID,
                    Battery = droneToCreate.Battery,//rand.Next(20, 41),
                    DroneLocation = new()
                    {
                        Lattitude = stationToCharge.Lattitude,
                        Longitude = stationToCharge.Longitude
                    },
                    Model = doDronelToCreate.Model,
                    ParcelId = 0,
                    Status = BO.DroneStatuses.maintenance,
                    Weight = (BO.WeightCategories)doDronelToCreate.MaxWeight
                };


                if (dronesList.Find(x => x.ID == newDrone.ID) != null)
                {
                    throw new BO.ContradictoryDataExeption("the drone already exist - the drone list in the BL is wrong"); 
                }
                dronesList.Add(newDrone);
            }
            catch (DO.AlreadyExistExeption x)
            {
                throw new BO.AlreadyExistExeption(x.Message, x); 
            }
            catch (DO.DoesntExistExeption x)
            {
                throw new BO.DoesntExistExeption(x.Message, x);
            }
        }
        #endregion

        #region GetDrone
        public BO.Drone GetDrone(int droneID)
        {
            DO.Drone doDroneToShow = new();
            try { doDroneToShow = DalAccess.GetDrone(droneID); }
            catch (DO.DoesntExistExeption x) { throw new BO.DoesntExistExeption(x.Message, x); }

            //build the drone to return
            /*BO.Drone boDroneToShow = new BO.Drone()*/

            //doDroneToShow.CopyPropertiesTo(boDroneToShow);
            BO.Drone boDroneToShow = new BO.Drone()
            {
                ID = doDroneToShow.ID,
                Model = doDroneToShow.Model,
                MaxWeight = (BO.WeightCategories)doDroneToShow.MaxWeight
            };

            //find the drone from the drones list
            BO.DroneToList droneFromList = dronesList.Find(x => x.ID == doDroneToShow.ID);
            if (droneFromList.ID == 0) //this check should be correct because the get function from dal
                throw new BO.ContradictoryDataExeption("drone found in DALdronList but doesnt exist in BLDroneList");

            boDroneToShow.Battery = droneFromList.Battery;
            boDroneToShow.DroneLocation = new()
            {
                Lattitude = droneFromList.DroneLocation.Lattitude,
                Longitude = droneFromList.DroneLocation.Longitude
            };
            boDroneToShow.Status = droneFromList.Status;

            //check if there is ParcelInDeliveryByDrone
            if (droneFromList.Status == BO.DroneStatuses.delivery/*droneFromList.ParcelId!=0*/)
            {try { boDroneToShow.ParcelInDeliveryByDrone = GetParcelInDelivery(droneFromList.ParcelId); } catch { throw; }}

            return boDroneToShow;
        }
        #endregion

        #region GetDroneList
        public IEnumerable<BO.DroneToList> GetDroneList() => (from drone in dronesList select drone).ToList();
        #endregion

        #region GetDroneInCharge
        //create a list of all the drones that are charging in the station
        private IEnumerable<BO.DroneInCharge> GetDroneInCharge(int stationID)
        {
            //List<BO.DroneInCharge> DroneInChargeToReturn = new List<BO.DroneInCharge>();
            //List<DO.DroneCharge> doDroneInChargeSlote = DalAccess.GetPartOfDroneCharge(x => (x.Stationld == stationID) && (x.IsInCharge == true)).ToList();

            //foreach (var item in boDroneInChargeSlote)
            //{ DroneInChargeToReturn.Add(new BO.DroneInCharge() { ID = item.ID, Battery = dronesList.Find(x => x.ID == item.ID).Battery }); }

            return (from charge in DalAccess.GetPartOfDroneCharge(x => (x.Stationld == stationID) && (x.IsInCharge == true))
                    select new BO.DroneInCharge()
                    {
                        ID = charge.Droneld,
                        Battery = dronesList.Find(x => x.ID == charge.Droneld).Battery
                    }).ToList();
           
        }
        #endregion

        #region GetPartOfDrone
        public IEnumerable<BO.DroneToList> GetPartOfDrone(Predicate<BO.DroneToList> check)
        {
            return (from drone in dronesList
                    where check(drone)
                    select drone).ToList();
        }
        #endregion

        #region amountOfDronesInChargeInTheStation

        /// <summary>
        /// returns the amount of drones charging in a specific location
        /// </summary>
        /// <remarks>includ a validation for the location recieved</remarks>
        /// <param name="stationLocation"></param>
        /// <returns></returns>
        int amountOfDronesInChargeInTheStation(BO.Location stationLocation)
        {
            try
            {
                //choose all the drones in maintenance and in the wanted location
                return dronesList.
                    Where<BO.DroneToList>(x => (x.Status == BO.DroneStatuses.maintenance)
                                                && x.DroneLocation.IsEquel(stationLocation))
                               .Count();
            }
            catch (BO.InvalidInputExeption)
            { return 0; }
        }
        #endregion

        #region pickUpDrone

        /// <summary>
        /// pick-up a parcel by drone - drone function
        /// </summary>
        /// <param name="droneID"></param>
        void pickUpDrone(int droneID)
        {
            //validation of the drone
            BO.DroneToList droneToPickUP = getDroneFromList(x => x.ID == droneID);
            BO.Drone droneFromBL = GetDrone(droneID); //throw DoesntExist

            DalAccess.PickUpByDrone(droneToPickUP.ParcelId); //send to dal to pick-up

            //update the drone
            dronesList.Remove(droneToPickUP);
            droneToPickUP.Battery -= ((int)droneToPickUP.DroneLocation.
                distanceLongitudeLatitude(droneFromBL.ParcelInDeliveryByDrone.PickUp))
                % (droneToPickUP.Battery - 40);
            droneToPickUP.DroneLocation = copyLocation(droneFromBL.ParcelInDeliveryByDrone.PickUp);
            dronesList.Add(droneToPickUP);
           
        }
        #endregion

        #region getDroneFromList
        /// <summary>
        /// get a IBL.BO.DroneToList from the drone list
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        BO.DroneToList getDroneFromList(Predicate<BO.DroneToList> check)
        {
            return dronesList.Find(x => check(x));
        }
        #endregion

        #region GetTheStationCharge
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public string GetTheStationCharge(int droneInChargeID)
        {
            DO.DroneCharge droneInCharge = DalAccess.GetPartOfDroneCharge(x => x.Droneld == droneInChargeID && x.IsInCharge == true).FirstOrDefault();
            if (droneInCharge.ID == 0)
                throw new BO.DoesntExistExeption("the drone is not charging");
            return GetStation(droneInCharge.Stationld).Name;
        }
        #endregion

        #region UpdateDrone
        public void UpdateDrone(int id, string model)
        {
            try
            {
                //sent to dal update
                DalAccess.UpdateDrone(id, model);

                //if the drone found-> update BL droneList
                BO.DroneToList droneToUpdate = dronesList.Find(x => x.ID == id);

                //update
                dronesList.Remove(droneToUpdate);
                droneToUpdate.Model = model;
                dronesList.Add(droneToUpdate);
                
            }
            catch (DO.DoesntExistExeption e)
            { throw new BO.DoesntExistExeption(e.Message, e); }
        }
        #endregion

        #region SendToCharge
        public void SendToCharge(int id)
        {
            //check if the drone exist ? maybe the dal layer should take care of this part ?
            BO.DroneToList droneToCharge = dronesList.Find(x => x.ID == id);

            if (droneToCharge == null)  throw new BO.DoesntExistExeption("the drone doesn't exist");
            if (droneToCharge.Status == BO.DroneStatuses.maintenance) throw new BO.AlreadyExistExeption("the drone is already charging");
            if (droneToCharge.Status == BO.DroneStatuses.delivery) throw new BO.InvalidInputExeption("the drone is in delivery. so it can not be sent to charge");
            if (droneToCharge.Battery < powerMinimumIfAvailable) throw new BO.BattaryExeption("there is not enough battery to send the drone to charge");

            //send to charge in the closest station
            try
            {
                //find the closest station
                //if the chargeslot >0!!!!!!!!!!!!!!!!
                int closestStatioID = findClosestStation(droneToCharge.DroneLocation, x => x.ChargeSlots > 0);
                DalAccess.SendToCharge(id, closestStatioID);


                Location location= copyLocation(GetStation(closestStatioID).StationLocation);
                //update the drone's details
                dronesList.Remove(droneToCharge);

                //the location of the drone is the location of the closest ststion
                droneToCharge.DroneLocation = location;
                //battery -> %20 of the distance between the station and the drone
                droneToCharge.Battery -= ((int)droneToCharge.DroneLocation.distanceLongitudeLatitude(location)) % (droneToCharge.Battery - 40);
                droneToCharge.Status = BO.DroneStatuses.maintenance;

                dronesList.Add(droneToCharge);
                
            }
            catch (DO.DoesntExistExeption x) { throw new BO.DoesntExistExeption(x.Message, x); }
            catch (BO.DoesntExistExeption e) { throw new BO.DoesntExistExeption("the close station doesnt have available charge slots", e); }
            catch(BO.InvalidInputExeption) { throw new BO.ContradictoryDataExeption("the location of the station wasn't supposed to throw an exception"); }
        }
        #endregion

        #region ReleaseFromCharge
        public void ReleaseFromCharge(int id, double timeOfChargeInHours)
        {
            //the time should be a DataTime type????????????
            BO.DroneToList droneToRelease = dronesList.Find(x => x.ID == id);
            if (droneToRelease == null) throw new BO.DoesntExistExeption("the drone doesn't exist");
            if (droneToRelease.Status != BO.DroneStatuses.maintenance)  throw new BO.AlreadyExistExeption("the drone isn't charging");
            
            try
            {
                DalAccess.ReleaseFromCharge(id);

                dronesList.Remove(droneToRelease);

                //update
                droneToRelease.Battery += (int)(timeOfChargeInHours * ChargePrecentagePerHoure); //increase the battery according to the time the drone was charging
                if(droneToRelease.Battery > 100) droneToRelease.Battery = 100; //round the result to fit in the max value of charge = 100%
                droneToRelease.Status = BO.DroneStatuses.available;

                dronesList.Add(droneToRelease);
                
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region delete
        #endregion
    }
}
