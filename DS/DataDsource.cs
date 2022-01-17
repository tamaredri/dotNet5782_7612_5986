using System;
using System.Collections.Generic;
using System.Linq;
using DO;


namespace DS
{
    public static class DataSource
    {
        internal static Random rand = new Random();
        #region all the lists
        public static List<Customer> CustomersList = new List<Customer>();
        public static List<Drone> DronesList = new List<Drone>();
        public static List<Parcel> ParcelsList = new List<Parcel>();
        //public static List<Parcel> SuccessfullyDeliveredParcelList = new List<Parcel>();
        public static List<Station> StationsList = new List<Station>();
        public static List<DroneCharge> ChargesList = new List<DroneCharge>();
        #endregion

        static DataSource()
        { Initialize(); }

        #region config class
        public class Config
        {
            public static int runningPackageNumber = 0;
            public static int runningStationNumber = 0;
            public static int runningDroneNumber = 0;
            public static int runningChargeNumber = 0;

            public static double powerMinimumIfAvailable = 0.05;
            public static double powerMinimumIfCarryLightWeight = 0.1;
            public static double powerMinimumIfCarryMiddleWeight = 0.15;
            public static double powerMinimumIfCarryHeavyWeight = 0.2;
            public static double ChargePrecentagePerHoure = 45;
        }
        #endregion
        #region initializer
        public static void Initialize()
        {
            #region initializing customer list:
            for (int i = 0; i < 10; i++)
            {
                CustomersList.Add(new Customer
                {
                    ID = 100000000 + i + 1,
                    Name = "" + ((char)(97 + i)),
                    Phone = "0" + rand.Next(0580000000, 0590000000),
                    Lattitude = 35.810873,
                    Longitude = 32.982540
                });
            }
            #endregion

            #region initializing base station list:
            for (int i = 0; i < 2; i++)
            {
                Config.runningStationNumber++;

                StationsList.Add(new Station
                {
                    ID = Config.runningStationNumber,
                    ChargeSlots = 5,
                    Lattitude = 34.981915,
                    Longitude = 31.713762,
                    Name = "a" + ((char)(97 + i))
                });
            }
            #endregion

            #region initializing drones list:
            for (int i = 0; i < 5; i++)
            {
                Config.runningDroneNumber++;

                DronesList.Add(new Drone
                {
                    ID = Config.runningDroneNumber,
                    MaxWeight = (WeightCategories)(i % 3),
                    Model = "Sky-Fly-" + (char)(97 + i) + "-" + i
                });
            }
            #endregion

            #region initializing parcel list:

            //unscheduled
            for (int i = 0; i < 5; i++)
            {
                Config.runningPackageNumber++;

                ParcelsList.Add(new Parcel
                {
                    ID = Config.runningPackageNumber,
                    SenderID = 100000000 + i + 1,
                    TargetID = 100000000 + 10 - i,
                    Weight = (WeightCategories)(i % 3),
                    Priority = (Prioritie)(i % 3),
                    Requested = DateTime.Now,
                    DroneID = 0
                });
            }

            //scheduled
            for (int i = 5; i < 6; i++)
            {
                Config.runningPackageNumber++;

                ParcelsList.Add(new Parcel
                {
                    ID = Config.runningPackageNumber,
                    SenderID = 100000000 + i + 1,
                    TargetID = 100000000 + 10 - i,
                    Weight = (from drone in DronesList where drone.ID == i - 4 select drone ).FirstOrDefault().MaxWeight,
                    Priority = (Prioritie)(i % 3),
                    Requested = DateTime.Now,
                    Scheduled = DateTime.Now,
                    DroneID = i-4
                });
            }

            //picked-up
            for (int i = 6; i < 9; i++)
            {
                Config.runningPackageNumber++;

                ParcelsList.Add(new Parcel
                {
                    ID = Config.runningPackageNumber,
                    SenderID = 100000000 + i + 1,
                    TargetID = 100000000 + 10 - i,
                    Weight = (from drone in DronesList where drone.ID == i - 4 select drone).FirstOrDefault().MaxWeight,
                    Priority = (Prioritie)(i % 3),
                    Requested = DateTime.Now,
                    Scheduled = DateTime.Now,
                    PickedUp = DateTime.Now,
                    DroneID = i-4
                });
            }
            Config.runningPackageNumber++;
            ParcelsList.Add(new Parcel
            {
                ID = Config.runningPackageNumber,
                SenderID = 100000000 + 10,
                TargetID = 100000000 + 1,
                Weight = (from drone in DronesList where drone.ID == 2 select drone).FirstOrDefault().MaxWeight,
                Priority = (Prioritie)(10 % 3),
                Requested = DateTime.Now,
                Scheduled = DateTime.Now,
                PickedUp = DateTime.Now,
                Delivered = DateTime.Now,
                DroneID = 2
            });

            #endregion
        }
        #endregion
    }
}