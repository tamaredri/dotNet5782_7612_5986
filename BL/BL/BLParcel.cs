using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BL
{
    public partial class BL
    {
        //------------------------Parcel------------------------
        #region CreateParcel
        public void CreateParcel(BO.Parcel parcelToCreate)
        {
            //check if the input is correct
            try
            { DalAccess.GetCustomer(parcelToCreate.Sender.ID); }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption("wrong sender ID", x); }
            try
            { DalAccess.GetCustomer(parcelToCreate.Target.ID); }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption("wrong target ID", x); }

            //BO Parcel-> DO Parcel
            DO.Parcel doParcelToCreate = new()
            {
                Priority = (DO.Prioritie)parcelToCreate.ParcelPriorities,
                Weight = (DO.WeightCategories)parcelToCreate.Weight,
                Requested = DateTime.Now,
                Scheduled = null,
                PickedUp = null,
                Delivered = null,
                DroneID = 0,
                SenderID = parcelToCreate.Sender.ID,
                TargetID = parcelToCreate.Target.ID
            };

            try
            { DalAccess.CreateParcel(doParcelToCreate); }
            catch (DO.AlreadyExistExeption X)
            { throw new BO.AlreadyExistExeption(X.Message, X); }
        }
        #endregion

        #region PairDroneParcel
        public void PairDroneParcel(int droneID)
        {
            BO.DroneToList droneToPair = GetDroneList().ToList<BO.DroneToList>().Find(x => x.ID == droneID);

            if (droneToPair == null) throw new BO.DoesntExistExeption("the drone doesn't exist");
            if (droneToPair.Status != BO.DroneStatuses.available) throw new BO.DoesntExistExeption("the drone is not available to do a delivery");

            List<BO.ParcelToList> parcelsList = GetPartOfParcel(x => x.Status == BO.ParcelStatuse.created).ToList();
            List<BO.ParcelToList> filteredParcelsList = new();
            BO.Parcel closestParcel = null;

            for (BO.Priorities priority = BO.Priorities.emergency; priority >= BO.Priorities.regular; priority--)
            {
                for (BO.WeightCategories weight = droneToPair.Weight; weight >= BO.WeightCategories.light; weight--)
                {
                    //all the parcels that are in the specific weight category, priority and the drone has enough battery to carry the parcel 
                    filteredParcelsList = parcelsList.FindAll(x => x.Priority == priority 
                                                                && x.Weight == weight 
                                                                && getDroneCapabilityToCarry(GetDrone(droneID), GetParcel(x.ID)) < droneToPair.Battery);

                    if (filteredParcelsList.Count == 0)
                        continue;
                    try
                    {
                        //the first parcel
                        closestParcel = GetParcel(filteredParcelsList[0].ID);

                        //find a closer parcel
                        foreach (var parcel in filteredParcelsList)
                        {
                            BO.Parcel parcelToCompare = GetParcel(parcel.ID);

                            if (droneToPair.DroneLocation.DistanceBetweenPlaces(GetCustomer(closestParcel.Sender.ID).LocationOfCustomer)
                                >
                                droneToPair.DroneLocation.DistanceBetweenPlaces(GetCustomer(parcelToCompare.Sender.ID).LocationOfCustomer))
                            { closestParcel = parcelToCompare; }

                        }//end of for-each to find the closest
                        break;
                    }
                    catch (BO.DoesntExistExeption x)
                    { throw new BO.ContradictoryDataExeption(x.Message + " this error is not suppose to happend. parcel/customer error", x); }

                }//end of for loop according to weight category
                if (closestParcel != null)
                    break;
            }//end of for loop according to priority


            //a parcel was not found
            if (closestParcel == null) throw new BO.DoesntExistExeption("there is no parcel available for the drone to carry");

            try
            {
                //send to dal object to pair the drone and the parcel
                DalAccess.ScheduleDroneParcel(closestParcel.ID, droneID);

                /*update the drone's properties*/
                dronesList.Remove(droneToPair);

                droneToPair.Status = BO.DroneStatuses.delivery;
                droneToPair.ParcelId = closestParcel.ID;

                dronesList.Add(droneToPair);
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region PickUpParcelByDrone
        public void PickUpParcelByDrone(int droneID)
        {
            try
            { 
                pickUpDrone(droneID);
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region DeliverParcel
        public void DeliverParcel(int droneID)
        {
            BO.DroneToList droneToDeliver = GetDroneList().ToList<BO.DroneToList>().Find(x => x.ID == droneID);
            BO.Parcel parcelToDeliver = GetParcel(droneToDeliver.ParcelId);

            if (droneToDeliver.Status != BO.DroneStatuses.delivery)
            {
                throw new BO.DoesntExistExeption("the drone is not paired to any parcel");
            }

            if (droneToDeliver.Battery < powerMinimumIfAvailable)
            {
                throw new BO.ContradictoryDataExeption("the battery is incorrect");
            }

            if (GetParcelList().ToList().Find(x => x.ID == droneToDeliver.ParcelId).Status != BO.ParcelStatuse.pickedUp)
            {
                throw new BO.ContradictoryDataExeption("the parcel wasn't picked-up by the drone");
            }

            try
            {
                DalAccess.DelivereParcel(droneToDeliver.ParcelId);

                dronesList.Remove(droneToDeliver);

                droneToDeliver.Battery -= (int)(GetCustomer(parcelToDeliver.Target.ID).LocationOfCustomer.
                                                      DistanceBetweenPlaces(GetCustomer(parcelToDeliver.Sender.ID).LocationOfCustomer)
                                                      * getPowerConsumption(parcelToDeliver.Weight));
                droneToDeliver.DroneLocation = new()
                {
                    Lattitude = GetCustomer(parcelToDeliver.Target.ID).LocationOfCustomer.Lattitude,
                    Longitude = GetCustomer(parcelToDeliver.Target.ID).LocationOfCustomer.Longitude
                }; 
                droneToDeliver.Status = BO.DroneStatuses.available;
                droneToDeliver.ParcelId = 0;
                dronesList.Add(droneToDeliver);
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region GetParcel
        public BO.Parcel GetParcel(int parcelID)
        {
            DO.Parcel doParcelToShow;
            try
            { doParcelToShow = DalAccess.GetParcel(parcelID); }
            catch (DO.DoesntExistExeption X) { throw new BO.DoesntExistExeption(X.Message, X); }

            BO.Parcel boParcelToShow = new()
            {
                ID = doParcelToShow.ID,
                Sender = GetCustomerForParcel(doParcelToShow, "SenderID"),
                Target = GetCustomerForParcel(doParcelToShow, "TargetID"),
                Weight = (BO.WeightCategories)doParcelToShow.Weight,
                ParcelPriorities = (BO.Priorities)doParcelToShow.Priority,
                CreateTime = doParcelToShow.Requested,
                ScheduleTime = doParcelToShow.Scheduled,
                PickUpTime = doParcelToShow.PickedUp,
                DelivereTime = doParcelToShow.Delivered,
                DroneToDeliverParcel = null
            };

            //if a drone was paired
            if (doParcelToShow.DroneID != 0)
            {
                BO.Drone boDrone = GetDrone(doParcelToShow.DroneID);

                //build the drone for the pardel
                boParcelToShow.DroneToDeliverParcel = new DroneForParcel()
                {
                    ID = boDrone.ID,
                    Battery = boDrone.Battery,
                    DroneToParcelLocation = new()
                    {
                        Lattitude = boDrone.DroneLocation.Lattitude,
                        Longitude = boDrone.DroneLocation.Longitude
                    }
                };
            }

            return boParcelToShow;
        }
        #endregion

        #region GetParcelList
        public IEnumerable<ParcelToList> GetParcelList()
        {
            List<DO.Parcel> doParcelList = DalAccess.GetParcelList().ToList();

            return (from parcel in doParcelList
                    let parcelFromBl = GetParcel(parcel.ID)
                    select new ParcelToList()
                    {
                        ID = parcelFromBl.ID,
                        Priority = parcelFromBl.ParcelPriorities,
                        Weight = parcelFromBl.Weight,
                        SenderName = parcelFromBl.Sender.Name,
                        TargetName = parcelFromBl.Target.Name,
                        //short if
                        Status = ((parcelFromBl.ScheduleTime == null) ?
                        ParcelStatuse.created : ((parcelFromBl.PickUpTime == null) ?
                        ParcelStatuse.pairedToDrone : ((parcelFromBl.DelivereTime == null) ?
                        ParcelStatuse.pickedUp : ParcelStatuse.delivered)))
                    }).ToList();
        }
        #endregion

        #region GetPartOfParcel
        public IEnumerable<ParcelToList> GetPartOfParcel(Predicate<ParcelToList> check)
        {
            var query = (from parcel in GetParcelList()
                         where check(parcel)
                         select parcel).ToList<ParcelToList>();
            return query;
        }
        #endregion

        #region GetListRecivedOrSentParcels
        private IEnumerable<ParcelInCustomer> GetListRecivedOrSentParcels(Predicate<DO.Parcel> chek, string endCustomer)
        {
            List<ParcelInCustomer> ParcelInCustomerToReturn = new();

            List<DO.Parcel> doParcelsRecievedOrSent = DalAccess.GetPartOfParcel(chek).ToList();

            foreach (var item in doParcelsRecievedOrSent)
            {
                BO.ParcelInCustomer parcelRecievedOrSent = new();
                parcelRecievedOrSent.ID = item.ID;
                parcelRecievedOrSent.Weight = (BO.WeightCategories)item.Weight;
                parcelRecievedOrSent.Priority = (BO.Priorities)item.Priority;

                try { parcelRecievedOrSent.EndCustomer = GetCustomerForParcel(item, endCustomer); } catch { throw; }

                //change the status according to the time
                if (item.Scheduled == null)
                    parcelRecievedOrSent.Status = ParcelStatuse.created;
                else if (item.PickedUp == null)
                    parcelRecievedOrSent.Status = ParcelStatuse.pairedToDrone;
                else if (item.Delivered == null)
                    parcelRecievedOrSent.Status = ParcelStatuse.pickedUp;
                else parcelRecievedOrSent.Status = ParcelStatuse.delivered;

                ParcelInCustomerToReturn.Add(parcelRecievedOrSent);
            }
            return ParcelInCustomerToReturn;
        }
        #endregion

        #region GetParcelInDelivery
        /// <summary>
        /// get the parcel that in delivery by drone
        /// </summary>
        /// <param name="parcelID"></param>
        /// <returns></returns>
        private ParcelInDelivery GetParcelInDelivery(int parcelID, Location droneLocation)
        {
            DO.Parcel doParcel = DalAccess.GetParcel(parcelID);

            if (doParcel.ID == 0 || doParcel.Scheduled == null || doParcel.DroneID == 0)
                throw new ContradictoryDataExeption("parcel id/schedual/droneID are incorrect");

            DO.Customer doSenderThisParcel = DalAccess.GetCustomer(doParcel.SenderID);
            DO.Customer doTargetThisParcel = DalAccess.GetCustomer(doParcel.TargetID);

            return new ParcelInDelivery()
            {
                ID = doParcel.ID,
                Priority = (BO.Priorities)doParcel.Priority,
                Weight = (BO.WeightCategories)doParcel.Weight,
                InDelivery = doParcel.PickedUp is not null,
                PickUp = new Location()
                {
                    Lattitude = doSenderThisParcel.Lattitude,
                    Longitude = doSenderThisParcel.Longitude
                },
                Sender = new CustomerForParcel()
                {
                    ID = doSenderThisParcel.ID,
                    Name = doSenderThisParcel.Name
                },
                Destination = new Location()
                {
                    Lattitude = doTargetThisParcel.Lattitude,
                    Longitude = doTargetThisParcel.Longitude
                },
                Target = new CustomerForParcel()
                {
                    ID = doTargetThisParcel.ID,
                    Name = doTargetThisParcel.Name
                },
                //find distance between sender and drone or between sender and target
                Distance = (doParcel.PickedUp is not null) ?
                         new Location()
                          {
                             Lattitude = droneLocation.Lattitude,
                             Longitude = droneLocation.Longitude
                         }
                         .DistanceBetweenPlaces
                         (new Location()
                         {
                             Lattitude = doTargetThisParcel.Lattitude,
                             Longitude = doTargetThisParcel.Longitude
                         }) : // else
                         new Location()
                         {
                             Lattitude = doSenderThisParcel.Lattitude,
                             Longitude = doSenderThisParcel.Longitude
                         }
                         .DistanceBetweenPlaces
                         (new Location()
                         {
                             Lattitude = droneLocation.Lattitude,
                             Longitude = droneLocation.Longitude
                         })
            };
        }
        #endregion

        #region delete
        public void DeleteParcel(int IDParcelToDelete)
        {
            try
            {
                DalAccess.DeleteParcel(IDParcelToDelete);
            }
            catch (Exception x)
            {
                throw new BO.InvalidInputExeption(x.Message);
            }
        }
        #endregion
    }
}


