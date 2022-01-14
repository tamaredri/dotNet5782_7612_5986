using System;
using BlApi;
using DalApi;
using System.Collections.Generic;
using System.Linq;
using BO;


namespace BL
{
    sealed partial class BL : IBL
    {
        #region singelton
        static readonly IBL instance = new BL();
        public static IBL Instance { get => instance; }

        internal IDal DalAccess = DalFactory.GetDal();
        #endregion

        #region
        double powerMinimumIfAvailable;
        double powerMinimumIfCarryLightWeight;
        double powerMinimumIfCarryMiddleWeight;
        double powerMinimumIfCarryHeavyWeight;
        double ChargePrecentagePerHoure;


        internal static Random rand;
        List<BO.DroneToList> dronesList = new();
        /// <summary>
        /// constructor
        /// </summary>
        #endregion


        BL()
        {
            rand = new Random();

            #region save the charging details
                //save the charging details
                double[] powerConsumption = DalAccess.GetPowerConsumptionByDrone();
                powerMinimumIfAvailable = powerConsumption[0];
                powerMinimumIfCarryLightWeight = powerConsumption[1];
                powerMinimumIfCarryMiddleWeight = powerConsumption[2];
                powerMinimumIfCarryHeavyWeight = powerConsumption[3];
                ChargePrecentagePerHoure = powerConsumption[4];

            #endregion

            List<DO.Drone> dalDronelList = (List<DO.Drone>)DalAccess.GetDroneList();
            BO.DroneToList DroneToBL = null;

            foreach (var drone in dalDronelList) //go over all the drones
            {
                //if a parcel is connected to the drone
                DO.Parcel parcel = DalAccess.GetPartOfParcel(x => x.DroneID == drone.ID && x.Delivered is null).FirstOrDefault();

                BO.DroneStatuses droneStatus = DroneStatuses.available;

                if (parcel.Equals(default(DO.Parcel))) //the parcel was delivered or no parcel was found
                {
                    //the drone is not paired to a parcel
                    droneStatus = (BO.DroneStatuses)(rand.Next(0, 2)); //status is a random value

                    if (droneStatus == BO.DroneStatuses.available)
                    {
                        //unauthorized access to a parcel that is not set to an object

                        //GetCustomer(DalAccess.GetsuccessfullyDeliveredParcelList().FirstOrDefault();

                        Location customerLocation = GetCustomer(DalAccess.GetsuccessfullyDeliveredParcelList().FirstOrDefault().TargetID).LocationOfCustomer;//random location from parcels that was delivered
                        DroneToBL = new DroneToList()
                        {
                            ID = drone.ID,
                            Model = drone.Model,
                            ParcelId = 0,
                            Weight = (BO.WeightCategories)rand.Next(0, 3),
                            Status = droneStatus,
                            DroneLocation =
                            new Location()
                            {
                                Longitude = customerLocation.Longitude,
                                Lattitude = customerLocation.Lattitude
                            },
                            Battery = rand.Next((int)powerConsumption[0], 101)
                        };
                    }
                    else if (droneStatus == BO.DroneStatuses.maintenance)
                    {
                        Location stationLocation = GetStation(GetStationList().FirstOrDefault().ID).StationLocation;

                        DroneToBL = new DroneToList()
                        {
                            ID = drone.ID,
                            Model = drone.Model,
                            ParcelId = 0,
                            Weight = (BO.WeightCategories)rand.Next(0, 3),
                            Status = BO.DroneStatuses.available,
                            DroneLocation = new Location()
                            {
                                Longitude = stationLocation.Longitude, 
                                Lattitude = stationLocation.Lattitude
                            },
                            Battery = 100
                        };
                    }
                }
                else if (parcel.PickedUp != null) //the parcel was picked up
                {
                    Location customersLocation = GetCustomer(parcel.SenderID).LocationOfCustomer;
                    
                    DroneToBL = new DroneToList()
                    {
                        ID = drone.ID,
                        Model = drone.Model,
                        ParcelId = parcel.ID,
                        Weight = (BO.WeightCategories)(parcel.Weight),
                        Status = BO.DroneStatuses.delivery,
                        DroneLocation =
                        new Location()
                        {
                            Longitude = customersLocation.Longitude,
                            Lattitude = customersLocation.Lattitude
                        },
                        Battery = rand.Next((int)powerConsumption[(int)(parcel.Weight) + 1], 101)
                    };
                }
                else if (parcel.Scheduled != null) //scheduled but not pickud up
                {
                    Location stationLocation = GetStation(findClosestStation(GetCustomer(parcel.SenderID).LocationOfCustomer, x => true)).StationLocation;
                    DroneToBL = new DroneToList()
                    {
                        ID = drone.ID,
                        Model = drone.Model,
                        ParcelId = parcel.ID,
                        Weight = (BO.WeightCategories)(parcel.Weight),
                        Status = BO.DroneStatuses.delivery,
                        DroneLocation =
                        new Location()
                        {
                            Longitude = stationLocation.Longitude,
                            Lattitude = stationLocation.Lattitude
                        },
                        Battery = rand.Next((int)powerConsumption[(int)(parcel.Weight) + 1], 101)
                    };
                }

                if (droneStatus == BO.DroneStatuses.maintenance)
                {
                    dronesList.Add(DroneToBL);
                    SendToCharge(DroneToBL.ID);
                    dronesList.Remove(DroneToBL);

                    DroneToBL.Battery = rand.Next(0, 21);
                    dronesList.Add(DroneToBL);
                }
                else dronesList.Add(DroneToBL);
            }
        }
        
        public int GetDroneRunnindNumber() { return DalAccess.GetDroneRunningNumber(); }
    }
}
