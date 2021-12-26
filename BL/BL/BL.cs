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

            #region build drones
            //get the list of all the droness from dal
            List<DO.Drone> dalDronelList = (List<DO.Drone>)DalAccess.GetDroneList();

            int index = 0;
            DO.Drone droneFromDAL = new();
            BO.DroneToList DroneToBL = null;
            #endregion

            #region schedualdrone to parcel
            //schedual 3 drone
            for (; index < 3; index++)
            {
                droneFromDAL = dalDronelList[index];
                //creat a new drone with the details of the drone from the DO drone
                DroneToBL = new BO.DroneToList
                {
                    ID = droneFromDAL.ID,
                    Model = droneFromDAL.Model,
                    Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
                    Status = BO.DroneStatuses.delivery,
                    ParcelId = DalAccess.GetParcelList().ToList().Find(x => x.Weight <= droneFromDAL.MaxWeight).ID,//[index].ID,
                    DroneLocation = new Location { Lattitude = 34.981915, Longitude = 31.713762 }
                };
                DroneToBL.Battery = rand.Next((int)powerConsumption[(int)DalAccess.GetParcel(DroneToBL.ParcelId).Weight + 1], 101);


                //schedule 3 parcels
                DalAccess.ScheduleDroneParcel(DroneToBL.ParcelId, DroneToBL.ID);
                //pick-up 2 parcels
                if (index > 0)
                {
                    DalAccess.PickUpByDrone(DroneToBL.ParcelId);
                    DroneToBL.DroneLocation = new Location { Lattitude = 35.810873, Longitude = 32.982540 };
                }
                //deliver 1 parcel
                if (index > 1)
                {
                    DalAccess.DelivereParcel(DroneToBL.ParcelId);
                    DroneToBL.Status = BO.DroneStatuses.available;
                    DroneToBL.ParcelId = 0;
                }

                //save the drone in the list
                dronesList.Add(DroneToBL);
            }
            #endregion

            #region send to charge 1 drone
            //send to charge 1 drone
            droneFromDAL = dalDronelList[index++];
            DroneToBL = new BO.DroneToList
            {
                ID = droneFromDAL.ID,
                Model = droneFromDAL.Model,
                Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
                Status = BO.DroneStatuses.maintenance,
                ParcelId = 0,
                DroneLocation = new Location { Lattitude = 34.981915, Longitude = 31.713762 },
                Battery = rand.Next(0, 21)
            };

            //send to charge
            DalAccess.SendToCharge(DroneToBL.ID, DalAccess.GetStationList().ToList()[0].ID);

            //save the drone in the list
            dronesList.Add(DroneToBL);

            #endregion

            #region 1 drone available
            //1 drone available
            droneFromDAL = dalDronelList[index];
            DroneToBL = new BO.DroneToList
            {
                ID = droneFromDAL.ID,
                Model = droneFromDAL.Model,
                Weight = (BO.WeightCategories)droneFromDAL.MaxWeight,
                Status = BO.DroneStatuses.available,
                ParcelId = 0,
                DroneLocation = new Location { Lattitude = 35.810873, Longitude = 32.982540 },
                Battery = rand.Next((int)powerMinimumIfAvailable, 101)
            };

            //save the drone in the list
            dronesList.Add(DroneToBL);
            #endregion
        }
        public int GetDroneRunnindNumber() { return DalAccess.GetDroneRunningNumber(); }


    }
}
