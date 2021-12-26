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
        public static List<Parcel> SuccessfullyDeliveredParcelList = new List<Parcel>();
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
            public static int runningCustomerNumber = 0;
            public static int runningChargeNumber = 0;

            public static double powerMinimumIfAvailable = 20;
            public static double powerMinimumIfCarryLightWeight = 50;
            public static double powerMinimumIfCarryMiddleWeight = 65;
            public static double powerMinimumIfCarryHeavyWeight = 75;
            public static double ChargePrecentagePerHoure = 45;
        }
        #endregion
        #region initializer
        public static void Initialize()
        {
            #region initializing customer list:
            for (int i = 0; i < 10; i++)//איתחול 10 לקוחות
            {
                Config.runningCustomerNumber++;// לפני הוספת לקוח- נעלה את המונ

                CustomersList.Add(new Customer
                {
                    ID = 100000000 + i,
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
                Config.runningStationNumber++;// add a station to the count

                StationsList.Add(new Station
                {
                    ID = Config.runningStationNumber,
                    ChargeSlots = 1,
                    Lattitude = 34.981915,
                    Longitude = 31.713762,
                    Name = "a" + ((char)(97 + i))
                });
            }
            #endregion

            #region initializing drones list:
            for (int i = 0; i < 5; i++)
            {

                Config.runningDroneNumber++;// לפני שנוסיף רחפן נעדכן שיש לנו רחפן נוסף

                DronesList.Add(new Drone
                {
                    ID = Config.runningDroneNumber,
                    MaxWeight = (WeightCategories)(i % 3),
                    Model = "Sky-Fly-" + (char)(97 + i) + "-" + i
                });



            }
            #endregion

            #region initializing parcel list:
            for (int i = 0; i < 10; i++)//איתחול 10 חבילות
            {
                Config.runningPackageNumber++; //increasing the running number before build a new parcel

                ParcelsList.Add(new Parcel
                {
                    ID = Config.runningPackageNumber,
                    SenderID = 100000000 + i,
                    TargetID = 100000000 + 9 - i,
                    Weight = (WeightCategories)(i % 3),
                    Priority = (Prioritie)(i % 3),
                    Requested = DateTime.Now,
                    DroneID = 0
                });
            }
            #endregion
        }
        #endregion
    }
}