using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using DS;

namespace Dal
{
    sealed class DalObject : IDal
    {
        #region singlton
        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }
        static DalObject() { }
        DalObject() { }
        #endregion

        //-----------------customer--------------
        #region AddCustomer
        public void CreateCustomer(Customer customerToCreate)
        {
            DataSource.Config.runningCustomerNumber++;

            if (DataSource.CustomersList.Find(x => x.ID == customerToCreate.ID).Equals(default(Customer)))
                throw new AlreadyExistExeption("the customer already exit");

            DataSource.CustomersList.Add(customerToCreate);
        }
        #endregion
        #region GetCustomer
        public Customer GetCustomer(int idToGet)
        {
            Customer customerToGet = (from customer in DataSource.CustomersList
                                      where customer.ID == idToGet
                                      select customer).FirstOrDefault();
            if (customerToGet.Equals(default(Customer)))
                throw new DoesntExistExeption("the customer doesn't exited");
            return customerToGet;
        }
        #endregion
        #region GetCustomersList
        public IEnumerable<Customer> GetCustomersList() => (from customer in DataSource.CustomersList
                                                            select customer).ToList();
        #endregion
        #region GetPartOfCustomer
        public IEnumerable<Customer> GetPartOfCustomer(Predicate<Customer> check) => (from customer in DataSource.CustomersList
                                                                                      where check(customer)
                                                                                      select customer).ToList<Customer>();
        #endregion
        #region UpdateCustomer
        public void UpdateCustomer(int customerID, int newPhone = 0, string newName = null)
        {
            Customer customerToUpdate = GetCustomer(customerID);

            DataSource.CustomersList.Remove(customerToUpdate);
            if (newPhone >= 100000000 && newPhone <= 999999999)
                customerToUpdate.Phone = "0" + newPhone.ToString();
            if (newName != null)
                customerToUpdate.Name = newName;
            DataSource.CustomersList.Add(customerToUpdate);
        }
        #endregion
        #region DeleteCustomer
        #endregion

        //-----------------drone-----------------
        #region CreateDrone
        public int CreateDrone(Drone droneToCreate)
        {
            DataSource.Config.runningDroneNumber++;
            droneToCreate.ID = DataSource.Config.runningDroneNumber;
            DataSource.DronesList.Add(droneToCreate);

            return DataSource.Config.runningDroneNumber; //the drone's ID
        }
        #endregion
        #region GetDrone
        public Drone GetDrone(int idToGet)
        {
            Drone droneToGet = (from drone in DataSource.DronesList
                                where drone.ID == idToGet
                                select drone).FirstOrDefault();
            if (droneToGet.Equals(default(Drone)))
                throw new DoesntExistExeption("GetDrone : the drone doesn't exist");
            return droneToGet;
        }
        #endregion
        #region GetDroneList
        public IEnumerable<Drone> GetDroneList() => (from drone in DataSource.DronesList select drone).ToList();
        #endregion
        #region GetPartOfDrone
        public IEnumerable<Drone> GetPartOfDrone(Predicate<Drone> check) => (from drone in DataSource.DronesList
                                                                             where check(drone)
                                                                             select drone).ToList<Drone>();
        #endregion
        #region GetPowerConsumptionByDrone
        public double[] GetPowerConsumptionByDrone()
        {
            return new double[5]
            {
               DataSource.Config.powerMinimumIfAvailable,
               DataSource.Config.powerMinimumIfCarryLightWeight,
               DataSource.Config.powerMinimumIfCarryMiddleWeight,
               DataSource.Config.powerMinimumIfCarryHeavyWeight,
               DataSource.Config.ChargePrecentagePerHoure
            };
        }
        #endregion
        #region GetDroneRunningNumber
        public int GetDroneRunningNumber() => DataSource.Config.runningDroneNumber; 
        #endregion
        #region UpdateDrone
        public void UpdateDrone(int id, string newModel)
        {
            Drone droneToUpdate = GetDrone(id);

            DataSource.DronesList.Remove(droneToUpdate);
            droneToUpdate.Model = newModel;
            DataSource.DronesList.Add(droneToUpdate);
        }
        #endregion
        #region SendToCharge
        public void SendToCharge(int droneIDToCharge, int stationIDToCharge)
        {
            Drone droneToCharge = GetDrone(droneIDToCharge);

            //send to charge in the station.
            updateChargeSlots(stationIDToCharge,
                                x =>
                                {
                                    if (x <= 0)
                                        throw new DoesntExistExeption("there are no charge slots available in the station");
                                    return --x;
                                });

            createChargeEntity(droneIDToCharge, stationIDToCharge);
        }
        #endregion
        #region ReleaseFromCharge
        public void ReleaseFromCharge(int droneIDToRelease)
        {
            GetDrone(droneIDToRelease);//doesnt exist exception 

            deleteChargeEntity(droneIDToRelease); //release
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
            DataSource.Config.runningChargeNumber++;
            
            DataSource.ChargesList.Add(new DroneCharge()
            {
                Droneld = droneIDToCharge,
                Stationld = stationIDToCharge,
                IsInCharge = true,
                TimeStart = DateTime.Now,
                ID = DataSource.Config.runningChargeNumber
            });
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
            DroneCharge chargeToUpdate = (from droneCharge in DataSource.ChargesList
                                          where droneCharge.Droneld == droneID
                                          select droneCharge).FirstOrDefault();
            if (chargeToUpdate.Equals(default(DroneCharge)))
                throw new DoesntExistExeption("the drone is not charging");

            //update in the station that the drone is released from charge
            updateChargeSlots(chargeToUpdate.Stationld, x => ++x);

            DataSource.ChargesList.Remove(chargeToUpdate);
            chargeToUpdate.IsInCharge = false;
            chargeToUpdate.TimeFinish = DateTime.Now;
            DataSource.ChargesList.Add(chargeToUpdate);
        }
        #endregion
        #region GetPartOfDroneCharge
        public IEnumerable<DroneCharge> GetPartOfDroneCharge(Predicate<DroneCharge> check)
        {
            var help= (from droneCharge in DataSource.ChargesList
                    where check(droneCharge)
                    select droneCharge).ToList();
            return help;
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
            GetStation(stationID);

            //choose all the drones in maintenance and in the wanted location
            return DataSource.ChargesList.
                    Where<DroneCharge>(x => x.IsInCharge == true && x.Stationld == stationID)
                    .Count();
        }
        #endregion
        #endregion

        //-----------------parcel-----------------
        #region CreateParcel
        public void CreateParcel(Parcel parcelToCreate)
        {
            DataSource.Config.runningPackageNumber++;
            parcelToCreate.ID = DataSource.Config.runningPackageNumber;
            DataSource.ParcelsList.Add(parcelToCreate);
        }
        #endregion
        #region GetParcel
        public Parcel GetParcel(int idToGet)
        {
            Parcel parcelToGet = (from parcel in DataSource.ParcelsList
                                  where parcel.ID == idToGet
                                  select parcel).FirstOrDefault();
            if (parcelToGet.Equals(default(Parcel)))
            {   //the parcel is not in the existing parcels, look for it in the delivered parcels
                parcelToGet = (from parcel in DataSource.SuccessfullyDeliveredParcelList
                               where parcel.ID == idToGet
                               select parcel).FirstOrDefault();
                if (parcelToGet.Equals(default(Parcel)))
                    throw new DoesntExistExeption("the parcel dosen't exited");
            }
            return parcelToGet;
        }
        #endregion
        #region GetParcelList
        public IEnumerable<Parcel> GetParcelList() => (from p in DataSource.ParcelsList select p).ToList();
        #endregion
        #region GetsuccessfullyDeliveredParcelList
        public IEnumerable<Parcel> GetsuccessfullyDeliveredParcelList() => (from s in DataSource.SuccessfullyDeliveredParcelList select s).ToList();
        #endregion
        #region GetPartOfParcel
        public IEnumerable<Parcel> GetPartOfParcel(Predicate<Parcel> check) => (from parcel in DataSource.ParcelsList
                                                                                where check(parcel)
                                                                                select parcel).ToList<Parcel>();
        #endregion
        #region ScheduleDroneParcel
        public void ScheduleDroneParcel(int idParcelToSchedule, int idDroneToSchedule)
        {
            Parcel parcelToSchedule = GetParcel(idParcelToSchedule);//doesnt exist exception

            GetDrone(idDroneToSchedule);//doesnt exist exception

            DataSource.ParcelsList.Remove(parcelToSchedule);
            parcelToSchedule.Scheduled = DateTime.Now;
            parcelToSchedule.DroneID = idDroneToSchedule;
            DataSource.ParcelsList.Add(parcelToSchedule);
        }
        #endregion
        #region PickUpByDrone
        public void PickUpByDrone(int idParcelToPickUp)
        {
            Parcel parcelToPickUp = GetParcel(idParcelToPickUp);//doesnt exist exception

            DataSource.ParcelsList.Remove(parcelToPickUp);
            parcelToPickUp.PickedUp = DateTime.Now;
            DataSource.ParcelsList.Add(parcelToPickUp);
        }
        #endregion
        #region DelivereParcel
        public void DelivereParcel(int idParcelToDelivere)
        {
            Parcel parcelToDeliver = GetParcel(idParcelToDelivere);//doesnt exist exception

            DataSource.ParcelsList.Remove(parcelToDeliver);
            parcelToDeliver.Delivered = DateTime.Now;
            DataSource.SuccessfullyDeliveredParcelList.Add(parcelToDeliver);
        }
        #endregion
        #region DeleteParcel
        #endregion

        //-----------------station-----------------
        #region CreateStation
        public void CreateStation(Station stationToCreate)
        {
            //check if there is a station with the same name and location as the wanted station to add
            if (!GetPartOfStation(x => x.Name == stationToCreate.Name).FirstOrDefault().Equals(default(Station)) ||
                !GetPartOfStation(x => (x.Lattitude == stationToCreate.Lattitude)
                                                   && (x.Longitude == stationToCreate.Longitude)).FirstOrDefault().Equals(default(Station)))
                throw new AlreadyExistExeption("the station already exist");

            DataSource.Config.runningStationNumber++;
            stationToCreate.ID = DataSource.Config.runningStationNumber;
            DataSource.StationsList.Add(stationToCreate);
        }
        #endregion
        #region GetStation
        public Station GetStation(int idToGet)
        {
            Station stationToGet = (from station in DataSource.StationsList
                                    where station.ID == idToGet
                                    select station).FirstOrDefault();
            if (stationToGet.Equals(default(Station)))
                throw new DoesntExistExeption("the station doesn't exist");
            return stationToGet;
        }
        #endregion
        #region GetStationList
        public IEnumerable<Station> GetStationList() => (from s in DataSource.StationsList select s).ToList();
        #endregion
        #region GetPartOfStation
        public IEnumerable<Station> GetPartOfStation(Predicate<Station> check) => (from station in DataSource.StationsList
                                                                                   where check(station)
                                                                                   select station).ToList<Station>();
        #endregion
        #region UpdateStation
        public void UpdateStation(int stationIDToUpdate, int newChargeSlots, string newName)
        {
            Station stationToUpdate = GetStation(stationIDToUpdate);

            if (newChargeSlots > 0)
            {
                int usedChargeSlots = getAmountOfUsedChargeSlots(stationIDToUpdate);
                updateChargeSlots(stationIDToUpdate, x => x = newChargeSlots - usedChargeSlots); //check
                stationToUpdate.ChargeSlots = newChargeSlots - usedChargeSlots;
            }

            if (newName != "")
            {
                DataSource.StationsList.Remove(stationToUpdate);
                stationToUpdate.Name = newName;
                DataSource.StationsList.Add(stationToUpdate);
            }
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
            Station stationToPair = GetStation(stationIDToCharge);//doesnt exist exception

            DataSource.StationsList.Remove(stationToPair);
            stationToPair.ChargeSlots = update(stationToPair.ChargeSlots);
            DataSource.StationsList.Add(stationToPair);

        }
        #endregion
        #endregion

    }
}
