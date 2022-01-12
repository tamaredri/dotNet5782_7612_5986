using System;
using DO;
using System.Collections.Generic;


namespace DalApi
{
    public interface IDal
    {
        //--------------------------------------station--------------------------------------
        #region station

        /// <summary>
        /// craeate a new Dal station
        /// </summary>
        /// <exception cref="AlreadyExistExeption">the station already exist</exception>
        /// <param name="stationToCreate"></param>
        public void CreateStation(Station stationToCreate);
        /// <summary>
        /// get a specific station
        /// </summary>
        /// <exception cref="DoesntExistExeption">the station doesn't exist</exception>
        /// <param name="idToGet"></param>
        /// <returns>the dal station requested</returns>
        public Station GetStation(int idToGet);
        /// <summary>
        /// get all the stations
        /// </summary>
        /// <returns>a list contains all the stations</returns>
        public IEnumerable<Station> GetStationList();
        /// <summary>
        /// get a part of the stations.
        /// choosing the stations according to a predicate sent to the function
        /// </summary>
        /// <param name="check">a boolian function to select the wanted stations</param>
        /// <returns>a new list of dal stations that implement the condition</returns>
        public IEnumerable<Station> GetPartOfStation(Predicate<Station> check);
        /// <summary>
        /// update the details of the station
        /// </summary>
        /// <remarks>to update: name- the name is not an empty string. charge slots- gratter or equal to 0</remarks>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="stationIDToUpdate"></param>
        /// <param name="newChargeSlots"></param>
        /// <param name="newName"></param>
        public void UpdateStation(int stationIDToUpdate, int newChargeSlots, string newName);

        #endregion
        //--------------------------------------drone----------------------------------------
        #region drone
        /// <summary>
        /// create a new Dal drone
        /// </summary>
        /// <exception cref="AlreadyExistExeption">the drone already exist</exception>
        /// <param name="droneToCreate"></param>
        public int CreateDrone(Drone droneToCreate);
        /// <summary>
        /// get a specific drone
        /// </summary>
        /// <exception cref="DoesntExistExeption">the drone doesn't exist</exception>
        /// <param name="idToGet"></param>
        /// <returns>the dal drone requested</returns>
        public Drone GetDrone(int idToGet);
        /// <summary>
        /// get all the drones
        /// </summary>
        /// <returns>the dal drone requested</returns>
        public IEnumerable<Drone> GetDroneList();
        /// <summary>
        /// get a part of the drones.
        /// choosing the drones according to a predicate sent to the function
        /// </summary>
        /// <param name="check">a boolian function to select the wanted drones</param>
        /// <returns>a new list of dal drones that implement the condition</returns>
        public IEnumerable<Drone> GetPartOfDrone(Predicate<Drone> check);
        /// <summary>
        /// get the power consumption details of the drones
        /// </summary>
        /// <returns>a list that contains the wated data:
        /// [0] = the minimum power the drone can function with.
        /// [1] = the minimum power the drone need to carry a light weight parcel
        /// [2] = the minimum power the drone need to carry a middle weight parcel
        /// [3] = the minimum power the drone need to carry a heavy weight parcel
        /// [4] = the precentage the drone can charge per hour</returns>
        public double[] GetPowerConsumptionByDrone();
        /// <summary>
        /// send a drone to cherge
        /// </summary>
        /// <exception cref="DoesntExistExeption">the drone doesn't exist</exception>
        /// <param name="droneIDToCharge"></param>
        /// <param name="stationIDToCharge"></param>
        public void SendToCharge(int droneIDToCharge, int stationIDToCharge);
        /// <summary>
        /// release a drone from charging
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="droneIDToReleas"></param>
        public void ReleaseFromCharge(int droneIDToReleas);
        /// <summary>
        ///get a part of the drone charge entities
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="check">predicate to choose the entities </param>
        /// <returns>Ienumerable of the wanted droneCharge entities</returns>
        public IEnumerable<DroneCharge> GetPartOfDroneCharge(Predicate<DroneCharge> check);
        /// <summary>
        /// updating the drone details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newModel"></param>
        public void UpdateDrone(int id, string newModel);

        public int GetDroneRunningNumber();

        #endregion
        //--------------------------------------customer--------------------------------------
        #region customer
        /// <summary>
        /// create a new Dal customer
        /// </summary>
        /// <exception cref="AlreadyExistExeption"></exception>"
        /// <param name="customerToCreate"></param>
        public void CreateCustomer(Customer customerToCreate);
        /// <summary>
        /// get a specific customer
        /// </summary>
        /// <exception cref="DoesntExistExeption">the customer doesnt exist</exception>
        /// <param name="idToGet"></param>
        /// <returns>the dal customer requested</returns>
        public Customer GetCustomer(int idToGet);
        /// <summary>
        /// get all the customers
        /// </summary>
        /// <returns>a list contains all the customers</returns>
        public IEnumerable<Customer> GetCustomersList();
        /// <summary>
        /// get a part of the customers.
        /// choosing the drones according to a predicate sent to the function
        /// </summary>
        /// <param name="check">a boolian function to select the wanted customers</param>
        /// <returns>a new list of dal customers that implement the condition</returns>
        public IEnumerable<Customer> GetPartOfCustomer(Predicate<Customer> check);
        /// <summary>
        /// update the details of the customer.
        /// update the name or the phone number of the customer
        /// </summary>
        /// <exception cref="DoesntExistExeption"></exception>"
        /// <param name="customerID"></param>
        /// <param name="newPhone"></param>
        /// <param name="newName"></param>
        public void UpdateCustomer(int customerID, int newPhone = 0, string newName = null);
        #endregion
        //--------------------------------------parcel----------------------------------------
        #region parcel
        /// <summary>
        /// create a new Dal parcel
        /// </summary>
        /// <exception cref="AlreadyExistExeption">the parcel already exist</exception>
        /// <param name="parcelToCreate"></param>
        public void CreateParcel(Parcel parcelToCreate);
        /// <summary>
        /// get a specific parcel
        /// </summary>
        /// <remarks>allows to ask for a parcel that was already delivered</remarks>
        /// <exception cref="DoesntExistExeption">the parcel doesnt exist</exception>
        /// <param name="idToGet"></param>
        /// <returns>the dal parcel requested</returns>
        public Parcel GetParcel(int idToGet);
        /// <summary>
        /// get all the parcels
        /// </summary>
        /// <returns>a list contains all the parcels</returns>
        public IEnumerable<Parcel> GetParcelList();
        /// <summary>
        /// get all the succesfully delivered parcels
        /// </summary>
        /// <returns>a list contains all the succesfully delivered parcels</returns>
        public IEnumerable<Parcel> GetsuccessfullyDeliveredParcelList();
        /// <summary>
        /// get a part of the parcels
        /// </summary>
        /// <param name="check">a boolian function to select the wanted parcels</param>
        /// <returns>a new list of dal parcels that implement the condition</returns>
        public IEnumerable<Parcel> GetPartOfParcel(Predicate<Parcel> check);
        /// <summary>
        /// schedule a prcel to a drone
        /// </summary>
        /// <remarks>update only the parcel</remarks>
        /// <exception cref="DoesntExistExeption">the parcel doesnt exist</exception>
        /// <param name="idParcelToSchedule"></param>
        /// <param name="idDroneToSchedule"></param>
        public void ScheduleDroneParcel(int idParcelToSchedule, int idDroneToSchedule);
        /// <summary>
        /// pick-up the parcel by a drone
        /// </summary>
        /// <remarks>update only the parcel</remarks>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="idParcelToPickUp"></param>
        public void PickUpByDrone(int idParcelToPickUp);
        /// <summary>
        /// delivere the parcel to the destination
        /// </summary>
        /// <remarks>update only the parcel</remarks>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="idParcelToDelivere"></param>
        public void DelivereParcel(int idParcelToDelivere);
        /// <summary>
        /// delete a parcel
        /// </summary>
        /// <remarks> throws an exception is the parcel is already paired to a drone</remarks>
        /// <exception cref="InvalidInputExeption"></exception>
        /// <param name="IDToDelete"></param>
        public void DeleteParcel(int IDToDelete);

        #endregion
    }
}
