﻿using System;
using DalApi;
using System.Xml.Linq;
using System.Collections.Generic;

using DO;


namespace Dal
{
    sealed class DalXml //: IDal
    {
        #region singlton
        //static readonly IDal instance = new DalXml();
        //public static IDal Instance { get => instance; }
        DalXml() 
        {
            XmlTools.LoadListFromXMLElement(configPath);
        }
        static DalXml() { }
        #endregion

        #region DS xml file

        string dronePath = "@droneXml.xml";
        string customerPath = "@customereXml.xml";
        string parcelPath = "@parcelXml.xml";
        string stationPath = "@stationXml.xml";
        string configPath = "@config.xml";

        #endregion

        #region customer XElement

        //-----------------customer--------------
        #region AddCustomer



        public void CreateCustomer(Customer customerToCreate)
        {

            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath);

            //DataSource.Config.runningCustomerNumber++;

            //if (DataSource.CustomersList.Find(x => x.ID == customerToCreate.ID).Name != null)
            //    throw new AlreadyExistExeption("the customer already exit");

            //DataSource.CustomersList.Add(customerToCreate);
        }
        #endregion
        #region GetCustomer
        public Customer GetCustomer(int idToGet)
        {
            //Customer customerToGet = (from customer in DataSource.CustomersList
            //                          where customer.ID == idToGet
            //                          select customer).FirstOrDefault();
            //if (customerToGet.Name == null)
            //    throw new DoesntExistExeption("the customer doesn't exited");
            //return customerToGet;
            return new Customer();
        }
        #endregion
        #region GetCustomersList
        public IEnumerable<Customer> GetCustomersList()
        {
            return new List<Customer>();
            //return (from customer in DataSource.CustomersList
            //        select customer).ToList();
        }
        #endregion
        #region GetPartOfCustomer
        public IEnumerable<Customer> GetPartOfCustomer(Predicate<Customer> check)
        {
            //return (from customer in DataSource.CustomersList
            //        where check(customer)
            //        select customer).ToList<Customer>();
            return new List<Customer>();
            
        }
        #endregion
        #region UpdateCustomer
        public void UpdateCustomer(int customerID, int newPhone = 0, string newName = null)
        {
            //Customer customerToUpdate = GetCustomer(customerID);

            //DataSource.CustomersList.Remove(customerToUpdate);
            //if (newPhone >= 100000000 || newPhone <= 999999999)
            //    customerToUpdate.Phone = "0" + newPhone.ToString();
            //if (newName != null)
            //    customerToUpdate.Name = newName;
            //DataSource.CustomersList.Add(customerToUpdate);
        }
        #endregion
        #region DeleteCustomer
        #endregion



        #endregion

        //-----------------drone-----------------
        #region CreateDrone
        public int CreateDrone(Drone droneToCreate)
        {
            //DataSource.Config.runningDroneNumber++;
            //droneToCreate.ID = DataSource.Config.runningDroneNumber;
            //DataSource.DronesList.Add(droneToCreate);

            //return DataSource.Config.runningDroneNumber; //the drone's ID
            return 4;
        }
        #endregion
        #region GetDrone
        public Drone GetDrone(int idToGet)
        {
            //Drone droneToGet = (from drone in DataSource.DronesList
            //                    where drone.ID == idToGet
            //                    select drone).FirstOrDefault();
            //if (droneToGet.ID == 0)
            //    throw new DoesntExistExeption("GetDrone : the drone doesn't exist");
            //return droneToGet;
            return new Drone();
        }
        #endregion
        #region GetDroneList
        public IEnumerable<Drone> GetDroneList()
        {
            return new List<Drone>();

            //return (from drone in DataSource.DronesList select drone).ToList();
        }
        #endregion
        #region GetPartOfDrone
        public IEnumerable<Drone> GetPartOfDrone(Predicate<Drone> check)
        {
            //return (from drone in DataSource.DronesList
            //        where check(drone)
            //        select drone).ToList<Drone>();
            return new List<Drone>();
        }
        #endregion
        #region GetPowerConsumptionByDrone
        public double[] GetPowerConsumptionByDrone()
        {
            return new double[5];
            //{
            //   DataSource.Config.powerMinimumIfAvailable,
            //   DataSource.Config.powerMinimumIfCarryLightWeight,
            //   DataSource.Config.powerMinimumIfCarryMiddleWeight,
            //   DataSource.Config.powerMinimumIfCarryHeavyWeight,
            //   DataSource.Config.ChargePrecentagePerHoure
            //};
        }
        #endregion
        #region GetDroneRunningNumber
        public int GetDroneRunningNumber()
        {
            return 1;//return DataSource.Config.runningDroneNumber; 
        }
        #endregion
        #region UpdateDrone
        public void UpdateDrone(int id, string newModel)
        {
            //Drone droneToUpdate = GetDrone(id);

            //DataSource.DronesList.Remove(droneToUpdate);
            //droneToUpdate.Model = newModel;
            //DataSource.DronesList.Add(droneToUpdate);
        }
        #endregion
        #region SendToCharge
        public void SendToCharge(int droneIDToCharge, int stationIDToCharge)
        {
            //Drone droneToCharge = GetDrone(droneIDToCharge);

            ////send to charge in the station.
            //updateChargeSlots(stationIDToCharge,
            //                    x =>
            //                    {
            //                        if (x <= 0)
            //                            throw new DoesntExistExeption("there are no charge slots available in the station");
            //                        return --x;
            //                    });

            //createChargeEntity(droneIDToCharge, stationIDToCharge);
        }
        #endregion
        #region ReleaseFromCharge
        public void ReleaseFromCharge(int droneIDToRelease)
        {
            //GetDrone(droneIDToRelease);//doesnt exist exception 

            //deleteChargeEntity(droneIDToRelease); //release
        }
        #endregion
        #region DeleteDrone
        #endregion


        //-----------------drone-charge-----------
        #region createChargeEntity
        /// <summary>
        /// create a new drone-charge entity
        /// </summary>
        /// <remarks>assuming the parameters are correct. 
        /// the function is private, so only class functions can use it</remarks>
        /// <param name="droneIDToCharge"></param>
        /// <param name="stationIDToCharge"></param>
        void createChargeEntity(int droneIDToCharge, int stationIDToCharge)
        {
            //DataSource.Config.runningChargeNumber++;
            //DroneCharge newCharge = new DroneCharge()
            //{
            //    Droneld = droneIDToCharge,
            //    Stationld = stationIDToCharge,
            //    IsInCharge = true,
            //    TimeStart = DateTime.Now,
            //    ID = DataSource.Config.runningChargeNumber
            //};
            //DataSource.ChargesList.Add(newCharge);
        }
        #endregion
        #region GetDroneCharge

        #endregion
        #region deleteChargeEntity
        /// <summary>
        /// the function removes a drone-charge entity
        /// </summary>
        /// <exception cref="DoesntExistExeption">if the drone isn't charging</exception>
        /// <param name="droneID"></param>
        void deleteChargeEntity(int droneID)
        {
            //DroneCharge chargeToUpdate = (from droneCharge in DataSource.ChargesList
            //                              where droneCharge.Droneld == droneID
            //                              select droneCharge).FirstOrDefault();
            //if (chargeToUpdate.ID == 0)
            //    throw new DoesntExistExeption("the drone is not charging");

            ////update in the station that the drone is released from charge
            //updateChargeSlots(chargeToUpdate.Stationld, x => ++x);

            //DataSource.ChargesList.Remove(chargeToUpdate);
            //chargeToUpdate.IsInCharge = false;
            //chargeToUpdate.TimeFinish = DateTime.Now;
            //DataSource.ChargesList.Add(chargeToUpdate);
        }
        #endregion
        #region GetPartOfDroneCharge
        public IEnumerable<DroneCharge> GetPartOfDroneCharge(Predicate<DroneCharge> check)
        {
            return new List<DroneCharge>();

            //return (from droneCharge in DataSource.ChargesList
            //        where check(droneCharge)
            //        select droneCharge).ToList();
        }
        #endregion
        #region private drone-charge functions
        #region getAmountOfUsedChargeSlots
        /// <summary>
        /// get the amount of the charging slots taken in a specific atation
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        int getAmountOfUsedChargeSlots(int stationID)
        {
            //GetStation(stationID);

            ////choose all the drones in maintenance and in the wanted location
            //return DataSource.ChargesList.
            //        Where<DroneCharge>(x => x.IsInCharge == true && x.Stationld == stationID)
            //        .Count();
            return 1;
        }
        #endregion
        #endregion

        //-----------------parcel-----------------
        #region CreateParcel
        public void CreateParcel(Parcel parcelToCreate)
        {
            //DataSource.Config.runningPackageNumber++;
            //parcelToCreate.ID = DataSource.Config.runningPackageNumber;
            //DataSource.ParcelsList.Add(parcelToCreate);
        }
        #endregion
        #region GetParcel
        public Parcel GetParcel(int idToGet)
        {
            //Parcel parcelToGet = (from parcel in DataSource.ParcelsList
            //                      where parcel.ID == idToGet
            //                      select parcel).FirstOrDefault();
            //if (parcelToGet.ID == 0)
            //{   //the parcel is not in the existing parcels, look for it in the delivered parcels
            //    parcelToGet = (from parcel in DataSource.SuccessfullyDeliveredParcelList
            //                   where parcel.ID == idToGet
            //                   select parcel).FirstOrDefault();
            //    if (parcelToGet.ID == 0)
            //        throw new DoesntExistExeption("the parcel dosen't exited");
            //}
            //return parcelToGet;
            return new Parcel();
        }
        #endregion
        #region GetParcelList
        public IEnumerable<Parcel> GetParcelList()
        {
            return new List<Parcel>();

            // return (from p in DataSource.ParcelsList select p).ToList();
        }
        #endregion
        #region GetsuccessfullyDeliveredParcelList
        public IEnumerable<Parcel> GetsuccessfullyDeliveredParcelList()
        {
            return new List<Parcel>();

            //return (from s in DataSource.SuccessfullyDeliveredParcelList select s).ToList();
        }
        #endregion
        #region GetPartOfParcel
        public IEnumerable<Parcel> GetPartOfParcel(Predicate<Parcel> check)
        {
            return new List<Parcel>();

            //return (from parcel in DataSource.ParcelsList
            //        where check(parcel)
            //        select parcel).ToList<Parcel>();
        }
        #endregion
        #region ScheduleDroneParcel
        public void ScheduleDroneParcel(int idParcelToSchedule, int idDroneToSchedule)
        {
            //Parcel parcelToSchedule = GetParcel(idParcelToSchedule);//doesnt exist exception

            //GetDrone(idDroneToSchedule);//doesnt exist exception

            //DataSource.ParcelsList.Remove(parcelToSchedule);
            //parcelToSchedule.Scheduled = DateTime.Now;
            //parcelToSchedule.DroneID = idDroneToSchedule;
            //DataSource.ParcelsList.Add(parcelToSchedule);
        }
        #endregion
        #region PickUpByDrone
        public void PickUpByDrone(int idParcelToPickUp)
        {
            //Parcel parcelToPickUp = GetParcel(idParcelToPickUp);//doesnt exist exception

            //DataSource.ParcelsList.Remove(parcelToPickUp);
            //parcelToPickUp.PickedUp = DateTime.Now;
            //DataSource.ParcelsList.Add(parcelToPickUp);
        }
        #endregion
        #region DelivereParcel
        public void DelivereParcel(int idParcelToDelivere)
        {
            //Parcel parcelToDeliver = GetParcel(idParcelToDelivere);//doesnt exist exception

            //DataSource.ParcelsList.Remove(parcelToDeliver);
            //parcelToDeliver.Delivered = DateTime.Now;
            //DataSource.SuccessfullyDeliveredParcelList.Add(parcelToDeliver);
        }
        #endregion
        #region DeleteParcel
        #endregion

        //-----------------station-----------------
        #region CreateStation
        public void CreateStation(Station stationToCreate)
        {
            ////check if there is a station with the same name and location as the wanted station to add
            //if (GetPartOfStation(x => x.Name == stationToCreate.Name).FirstOrDefault().Name != null ||
            //    GetPartOfStation(x => (x.Lattitude == stationToCreate.Lattitude)
            //                                       && (x.Longitude == stationToCreate.Longitude)).FirstOrDefault().Name != null)
            //    throw new AlreadyExistExeption("the station already exist");

            //DataSource.Config.runningStationNumber++;
            //stationToCreate.ID = DataSource.Config.runningStationNumber;
            //DataSource.StationsList.Add(stationToCreate);
        }
        #endregion
        #region GetStation
        public Station GetStation(int idToGet)
        {
            //Station stationToGet = (from station in DataSource.StationsList
            //                        where station.ID == idToGet
            //                        select station).FirstOrDefault();
            //if (stationToGet.Name == null)
            //    throw new DoesntExistExeption("the station doesn't exist");
            //return stationToGet;
            return new Station();
        }
        #endregion
        #region GetStationList
        public IEnumerable<Station> GetStationList()
        {
            //return (from s in DataSource.StationsList select s).ToList();
            ////return DataSource.StationsList.FindAll(x => x.ID != 0);
           return new List<Station>();

        }
        #endregion
        #region GetPartOfStation
        public IEnumerable<Station> GetPartOfStation(Predicate<Station> check)
        {
            //return (from station in DataSource.StationsList
            //        where check(station)
            //        select station).ToList<Station>();
            return new List<Station>();

        }
        #endregion
        #region UpdateStation
        public void UpdateStation(int stationIDToUpdate, int newChargeSlots, string newName)
        {
            //Station stationToUpdate = GetStation(stationIDToUpdate);

            //if (newChargeSlots >= 0)
            //{
            //    int usedChargeSlots = getAmountOfUsedChargeSlots(stationIDToUpdate);
            //    updateChargeSlots(stationIDToUpdate, x => x = newChargeSlots - usedChargeSlots); //check
            //}

            //if (newName != null)
            //{
            //    DataSource.StationsList.Remove(stationToUpdate);
            //    stationToUpdate.Name = newName;
            //    DataSource.StationsList.Add(stationToUpdate);
            //}
        }
        #endregion
        #region DeleteStation
        #endregion
        #region private station functions
        #region updateChargeSlots
        /// <summary>
        /// updating the amount of the charging slots according to an action chosen by you
        /// </summary>
        /// <remarks></remarks>
        /// <param name="stationIDToCharge"></param>
        /// <param name="update"></param>
        void updateChargeSlots(int stationIDToCharge, Converter<int, int> update)
        {
            //Station stationToPair = GetStation(stationIDToCharge);//doesnt exist exception

            //DataSource.StationsList.Remove(stationToPair);
            //stationToPair.ChargeSlots = update(stationToPair.ChargeSlots);
            //DataSource.StationsList.Add(stationToPair);

        }
        #endregion
        #endregion
    }
}