using System;
using System.Collections.Generic;
using BO;

namespace BlApi
{
    public interface IBL
    {
        //--------------------------------------station--------------------------------------
        #region station
        /// <summary>
        /// create a new BL station
        /// </summary>
        /// <exception cref="AlreadyExistExeption"></exception>
        /// <exception cref="InvalidInputExeption"></exception>
        /// <param name="stationToCreate"></param>
        public void CreateStation(Station stationToCreate);
        /// <summary>
        /// get a specific station
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="stationID"></param>
        /// <returns>a new BL station entity</returns>
        public Station GetStation(int stationID);
        /// <summary>
        /// get a list of all the existing stations
        /// </summary>
        /// <exception cref=""
        /// <returns>a list of all the stations</returns>
        public IEnumerable<StationToList> GetStationList();
        /// <summary>
        /// get a part of the stations according to a condition
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public IEnumerable<StationToList> GetPartOfStation(Predicate<StationToList> check);
        /// <summary>
        /// update the details of the station
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>"
        /// <exception cref="InvalidInputExeption"></exception>"
        /// <param name="stationID"></param>
        /// <param name="newChargeSlots"></param>
        /// <param name="newName"></param>
        public void UpdateStation(int stationID, int newChargeSlots, string newName);
        #endregion

        //--------------------------------------drone----------------------------------------
        #region drone
        /// <summary>
        /// create a new BL drone
        /// </summary>
        /// <exception cref="AlreadyExistExeption"></exception>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="droneToCreate"></param>
        public void CreateDrone(Drone droneToCreate, int idStationToCharge);
        /// <summary>
        /// get a specific drone
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <exception cref=""></exception>
        /// <exception cref=""></exception>
        /// <param name="droneID"></param>
        /// <returns>a new BL drone</returns>
        public Drone GetDrone(int droneID);
        /// <summary>
        /// get a list of drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> GetDroneList();
        /// <summary>
        /// update the details of the drone
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDrone(int id, string model);
        /// <summary>
        /// send the drone to charge
        /// </summary>
        /// <remarks>an exception will be throne if:  
        /// the drone is in charge already or
        /// the drone is in delivery or
        /// the battery is not enough to send to charge</remarks>
        /// <exception cref="IDAL.DO.DoesntExistExeption"></exception>"
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="AlreadyExistExeption"></exception>"
        /// <exception cref="InvalidInputExeption"></exception>"
        /// <exception cref="BattaryExeption"></exception>"
        /// <param name="id"></param>
        public void SendToCharge(int id);
        /// <summary>
        /// stop the charge and update the battery according to the time the drone was charging
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>"
        /// <exception cref="AlreadyExistExeption"></exception>"
        /// <param name="id"></param>
        /// <param name="timeOfChargeInHours"></param>
        public void ReleaseFromCharge(int id, double timeOfChargeInHours);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public IEnumerable<DroneToList> GetPartOfDrone(Predicate<DroneToList> check);

        public int GetDroneRunnindNumber();

        public string GetTheStationCharge(int droneInChargeID);

        #endregion

        //--------------------------------------customer-------------------------------------
        #region customer
        /// <summary>
        /// create a new BL customer
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="customerToCreate"></param>
        public void CreateCustomer(Customer customerToCreate);
        /// <summary>
        /// get a specific customer
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>"
        /// <param name="customerID"></param>
        /// <returns>a new BL customer entity</returns>
        public Customer GetCustomer(int customerID);
        /// <summary>
        /// get the list of all the customers
        /// </summary>
        /// <returns>list of BL customer</returns>
        public IEnumerable<CustomerToList> GetCustomerList();
        /// <summary>
        /// update the details of a customer
        /// </summary>
        /// <exception cref="InvalidInputExeption"></exception>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="customreIDToUpdate"></param>
        /// <param name="newPhone"></param>
        /// <param name="newName"></param>
        public void UpdateCustomer(int customreID, int newPhone, string newName);

        #endregion

        //--------------------------------------parcel---------------------------------------
        #region parcel
        /// <summary>
        /// create a new BL parcel
        /// </summary>
        /// <remarks>the valid properties in the input are: 
        /// sender and tanget ID, 
        /// weight category 
        /// and priority</remarks>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="AlreadyExistExeption"></exception>
        /// <param name="parcelToCreate"></param>
        public void CreateParcel(Parcel parcelToCreate);
        /// <summary>
        /// get a spedific parcel
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>"
        /// <param name="parcelID"></param>
        /// <returns>the requested BL parcel</returns>
        public Parcel GetParcel(int parcelID);
        /// <summary>
        /// get the list of all the parcels
        /// </summary>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <returns>a list of BL parcels</returns>
        public IEnumerable<ParcelToList> GetParcelList();
        /// <summary>
        /// get a part of the parcels according to a condition
        /// </summary>
        /// <param name="check"></param>
        /// <returns>a part of the parcels</returns>
        public IEnumerable<ParcelToList> GetPartOfParcel(Predicate<ParcelToList> check);
        /// <summary>
        /// pair a drone to a matching parcel
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="BattaryExeption"></exception>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <param name="droneID"></param>
        public void PairDroneParcel(int droneID);
        /// <summary>
        /// send the drone to pick-up the parcel paired to him
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <param name="droneID"></param>
        public void PickUpParcelByDrone(int droneID);
        /// <summary>
        /// mark that the drone arrived to the destination with the parcel
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <exception cref="ContradictoryDataExeption"></exception>
        /// <param name="droneID"></param>
        public void DeliverParcel(int droneID);
        #endregion
    }
}
