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
            //get the drone
            BO.DroneToList droneToPair = GetDroneList().ToList<BO.DroneToList>().Find(x => x.ID == droneID);

            if (droneToPair == null) throw new BO.DoesntExistExeption("the drone doesn't exist");
            if (droneToPair.Status != BO.DroneStatuses.available) throw new BO.DoesntExistExeption("the drone is not available to do a delivery");
            if (droneToPair.Battery < powerMinimumIfAvailable) throw new BO.BattaryExeption("there is not enough battery to make a delivery.\nit is recommended to send the drone to charge as soon as possible");

            //take all the parcels that was created but not paired
            List<BO.ParcelToList> parcelsList = GetPartOfParcel(x => x.Status == BO.ParcelStatuse.created).ToList();

            BO.Parcel closestParcel = null;

            //find a parcel that the drone can carry
            //for each priority type, in a decsending order
            for (BO.Priorities priority = BO.Priorities.emergency; priority >= BO.Priorities.regular; priority--)
            {
                //find all the parcels that are in the specific priority category
                List<BO.ParcelToList> filteredParcelsList = parcelsList.FindAll(x => x.Priority == priority);

                //no parcel was found
                if (filteredParcelsList.Count == 0)
                    continue;

                //for each wight category, in a decsending order
                for (BO.WeightCategories weight = droneToPair.Weight; weight >= BO.WeightCategories.light; weight--)
                {
                    /*the drone doesn't have enough battery to carry the parcel*/
                    if (droneToPair.Battery < DalAccess.GetPowerConsumptionByDrone()[(int)weight + 1])
                        continue;

                    //find all the parcels that are in the specific weight category
                    filteredParcelsList = filteredParcelsList.FindAll(x => x.Weight == weight);

                    //no parcel was found
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

                            //the distance between the drone location and the location of the sender of the parcel since the parsel is located in the sender's location
                            //                                                      the sender -  the id of the sender -  the location of the wanted customer
                            if (droneToPair.DroneLocation.distanceLongitudeLatitude((GetCustomer(closestParcel.Sender.ID)).LocationOfCustomer)
                                >
                                //                                                  the sender -  the id of the sender -  the location of the wanted customer
                                droneToPair.DroneLocation.distanceLongitudeLatitude((GetCustomer(parcelToCompare.Sender.ID)).LocationOfCustomer))
                            { closestParcel = parcelToCompare; }

                        }//end of for-each to find the closest
                    }
                    catch (BO.DoesntExistExeption x)
                    { throw new BO.ContradictoryDataExeption(x.Message + " this error is not suppose to happend. parcel/customer error", x); }

                }//end of for loop according to weight category

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
            //find the drone
            BO.Drone droneFromBL = GetDrone(droneID); //throw DoesntExist

            if (droneFromBL.Status != BO.DroneStatuses.delivery)  throw new BO.InvalidInputExeption("the drone is not paired to any parcel");
            if (droneFromBL.Battery < powerMinimumIfAvailable) throw new BO.ContradictoryDataExeption("the battery is incorrect");
            if (droneFromBL.ParcelInDeliveryByDrone.InDelivery) throw new BO.ContradictoryDataExeption("the parcel is already in delivery although the drone haven't picked it up yet");
            //^the parcel status 'inDelivery' is incorrect. it is true eventhogh the drone didnt pick him up yet!!!!!!!!!!!
            //check this exception!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //if (droneFromBL.ParcelInDeliveryByDrone.PickUp != null) throw new IBL.BO.ContradictoryDataExeption("the parcel pick-up location is already set");

            DroneToList dtoneFromDronesList = dronesList.Find(x => x.ID == droneID);

            try
            { 
                pickUpDrone(droneID);

                /*update the drone's properties*/
                dronesList.Remove(dtoneFromDronesList);

                //take the location of the parcel and the location of the drone and update the battery according to it
                dtoneFromDronesList.Battery -= ((int)droneFromBL.ParcelInDeliveryByDrone.PickUp.distanceLongitudeLatitude(dtoneFromDronesList.DroneLocation)) % 10;

                dronesList.Add(dtoneFromDronesList);
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region DeliverParcel
        public void DeliverParcel(int droneID)
        {
            //find the drone
            BO.DroneToList droneToDeliver = GetDroneList().ToList<BO.DroneToList>().Find(x => x.ID == droneID);

            if (droneToDeliver.Status != BO.DroneStatuses.delivery) throw new BO.DoesntExistExeption("the drone is not paired to any parcel");
            if (droneToDeliver.Battery < powerMinimumIfAvailable) throw new BO.ContradictoryDataExeption("the battery is incorrect");

            //get the parcel the drone need to pick-up
            BO.Parcel parcelToDeliver = GetParcel(droneToDeliver.ParcelId);

            if (GetParcelList().ToList().Find(x => x.ID == droneToDeliver.ParcelId).Status != BO.ParcelStatuse.pickedUp)
                throw new BO.ContradictoryDataExeption("the parcel wasn't picked-up by the drone");

            try
            {
                //deliver in the dal layer
                DalAccess.DelivereParcel(droneToDeliver.ParcelId);

                dronesList.Remove(droneToDeliver);

                droneToDeliver.Battery -= ((int)droneToDeliver.DroneLocation.
                    distanceLongitudeLatitude((GetCustomer(parcelToDeliver.Sender.ID)).LocationOfCustomer))
                    % (droneToDeliver.Battery - 40);
                droneToDeliver.DroneLocation = new()
                {
                    Lattitude = (GetCustomer(parcelToDeliver.Target.ID)).LocationOfCustomer.Lattitude,
                    Longitude = (GetCustomer(parcelToDeliver.Target.ID)).LocationOfCustomer.Longitude
                }; //take the sender's coordinates
                droneToDeliver.Status = BO.DroneStatuses.available;

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
                //chek if the sender exist????????? func GetCustomerForParcel check
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
            
                //check if there was an error in the deais of the parcel
                //find sender name
                //try { DalAccess.GetCustomer(item.SenderID); }
                //catch (DO.DoesntExistExeption x) { throw new ContradictoryDataExeption("the sender customer not found but he sent parcel", x); }
                ////find target name
                //try { DalAccess.GetCustomer(item.TargetID); }
                //catch (DO.DoesntExistExeption x) { throw new ContradictoryDataExeption("the target customer not found but there is a parcel that sent to him", x); }

              
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
            //list to return
            List<ParcelInCustomer> ParcelInCustomerToReturn = new();

            //get dal list of all parcel that recieved or sent to the customer 
            List<DO.Parcel> doParcelsRecievedOrSent = DalAccess.GetPartOfParcel(chek).ToList();

            //create a list of all the parcels the customer recieved
            

            //save the parcels recived in the customer's list
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
        /// 
        /// </summary>
        /// <param name="parcelID"></param>
        /// <returns></returns>
        private ParcelInDelivery GetParcelInDelivery(int parcelID)
        {
            //find the parcel that is in delivery by the drone
            DO.Parcel doParcel = DalAccess.GetParcel(parcelID);
            if (doParcel.ID == 0 || doParcel.Scheduled == null || doParcel.DroneID == 0)
                throw new ContradictoryDataExeption("parcel id/schedual/droneID are incorrect");
            //find the location of the sender:
            DO.Customer doSenderThisParcel = DalAccess.GetCustomer(doParcel.SenderID);
            DO.Customer doTargetThisParcel = DalAccess.GetCustomer(doParcel.TargetID);
            //build the ParcelInDeliveryByDrone
            ParcelInDelivery parcelInDeLInDeliveryToReturn = new()
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
                }
            };

            //find distance between sender and drone or between sender and target
            //Location droneLocation = GetDrone(doParcel.DroneID).DroneLocation;
            //if (doParcel.PickedUp is not null)
            //{
            //    parcelInDeLInDeliveryToReturn.Distance =
            //            new Location()
            //            {
            //                Lattitude = droneLocation.Lattitude,
            //                Longitude = droneLocation.Longitude
            //            }
            //            .distanceLongitudeLatitude
            //            (new Location()
            //            {
            //                Lattitude = doSenderThisParcel.Lattitude,
            //                Longitude = doSenderThisParcel.Longitude
            //            });
            //}
            //else
            //{
            //    parcelInDeLInDeliveryToReturn.Distance =
            //            new Location()
            //            {
            //                Lattitude = doSenderThisParcel.Lattitude,
            //                Longitude = doSenderThisParcel.Longitude
            //            }
            //            .distanceLongitudeLatitude
            //            (new Location()
            //            {
            //                Lattitude = droneLocation.Lattitude,
            //                Longitude = droneLocation.Longitude
            //            });
            //}
            //find distance between sender and target
            parcelInDeLInDeliveryToReturn.Distance =
                    new Location()
                    {
                        Lattitude = doSenderThisParcel.Lattitude,
                        Longitude = doSenderThisParcel.Longitude
                    }
                    .distanceLongitudeLatitude
                    (new Location()
                    {
                        Lattitude = doTargetThisParcel.Lattitude,
                        Longitude = doTargetThisParcel.Longitude
                    });
            return parcelInDeLInDeliveryToReturn;
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


