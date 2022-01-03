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

            #region old pair
            /*
            //#region build drones
            ////get the list of all the droness from dal

            //int index = 0;
            //DO.Drone droneFromDAL = new();
            //#endregion

            //#region schedualdrone to parcel
            ////schedual 3 drone
            //for (; index < 3; index++)
            //{
            //    droneFromDAL = dalDronelList[index];
            //    //creat a new drone with the details of the drone from the DO drone
            //    DroneToBL = new BO.DroneToList
            //    {
            //        ID = droneFromDAL.ID,
            //        Model = droneFromDAL.Model,
            //        Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
            //        Status = BO.DroneStatuses.delivery,
            //        ParcelId = DalAccess.GetParcelList().ToList().Find(x => x.Weight <= droneFromDAL.MaxWeight).ID,//[index].ID,
            //        DroneLocation = new Location { Lattitude = 34.981915, Longitude = 31.713762 }
            //    };
            //    DroneToBL.Battery = rand.Next((int)powerConsumption[(int)DalAccess.GetParcel(DroneToBL.ParcelId).Weight + 1], 101);


            //    //schedule 3 parcels
            //    DalAccess.ScheduleDroneParcel(DroneToBL.ParcelId, DroneToBL.ID);
            //    //pick-up 2 parcels
            //    if (index > 0)
            //    {
            //        DalAccess.PickUpByDrone(DroneToBL.ParcelId);
            //        DroneToBL.DroneLocation = new Location { Lattitude = 35.810873, Longitude = 32.982540 };
            //    }
            //    //deliver 1 parcel
            //    if (index > 1)
            //    {
            //        DalAccess.DelivereParcel(DroneToBL.ParcelId);
            //        DroneToBL.Status = BO.DroneStatuses.available;
            //        DroneToBL.ParcelId = 0;
            //    }

            //    //save the drone in the list
            //    dronesList.Add(DroneToBL);
            //}
            //#endregion

            //#region send to charge 1 drone
            ////send to charge 1 drone
            //droneFromDAL = dalDronelList[index++];
            //DroneToBL = new BO.DroneToList
            //{
            //    ID = droneFromDAL.ID,
            //    Model = droneFromDAL.Model,
            //    Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
            //    Status = BO.DroneStatuses.maintenance,
            //    ParcelId = 0,
            //    DroneLocation = new Location { Lattitude = 34.981915, Longitude = 31.713762 },
            //    Battery = rand.Next(0, 21)
            //};

            ////send to charge
            //DalAccess.SendToCharge(DroneToBL.ID, DalAccess.GetStationList().ToList()[0].ID);

            ////save the drone in the list
            //dronesList.Add(DroneToBL);

            //#endregion

            //#region 1 drone available
            ////1 drone available
            //droneFromDAL = dalDronelList[index];
            //DroneToBL = new BO.DroneToList
            //{
            //    ID = droneFromDAL.ID,
            //    Model = droneFromDAL.Model,
            //    Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
            //    Status = BO.DroneStatuses.available,
            //    ParcelId = 0,
            //    DroneLocation = new Location { Lattitude = 35.810873, Longitude = 32.982540 },
            //    Battery = rand.Next((int)powerMinimumIfAvailable, 101)
            //};

            ////save the drone in the list
            //dronesList.Add(DroneToBL);
            //#endregion
            */
            #endregion


            foreach (var drone in dalDronelList) //go over all the drones
            {
                //if a parcel is connected to the drone
                DO.Parcel parcel = DalAccess.GetPartOfParcel(x => x.DroneID == drone.ID).FirstOrDefault();

                if (parcel.Equals(default(Parcel))) //no parcel was found
                {
                    //the drone is not paired to a parcel
                    BO.DroneStatuses droneStatus = (BO.DroneStatuses)(rand.Next(0, 2)); //status is a random value

                    if (droneStatus == BO.DroneStatuses.available)
                    {
                        Location customerLocation = GetCustomer(DalAccess.GetsuccessfullyDeliveredParcelList().FirstOrDefault().TargetID).LocationOfCustomer;//random location from parcels that was delivered
                        DroneToBL = new DroneToList()
                        {
                            ID = drone.ID,
                            Model = drone.Model,
                            ParcelId = 0,
                            Weight = (BO.WeightCategories)rand.Next(0, 4),
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
                        DroneToBL = new DroneToList()
                        {
                            ID = drone.ID,
                            Model = drone.Model,
                            ParcelId = 0,
                            Weight = (BO.WeightCategories)rand.Next(0, 4),
                            Status = droneStatus,
                            Battery = rand.Next(0, 21)
                        };
                    }
                }
                else if (parcel.Delivered != null) //the parcel was picked up
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
                else if (parcel.PickedUp != null) //scheduled but not pickud up
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

                if (DroneToBL.Status == BO.DroneStatuses.maintenance)
                {
                    DroneToBL.Status = BO.DroneStatuses.available;
                    dronesList.Add(DroneToBL);
                    SendToCharge(DroneToBL.ID);
                }
                else dronesList.Add(DroneToBL);
            }
        }
        

        public int GetDroneRunnindNumber() { return DalAccess.GetDroneRunningNumber(); }
    }
}
