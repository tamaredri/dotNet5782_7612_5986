using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BL
{
    public static class ExtansionMethods
    {
        #region to string for a list
        public static string ToString<T>(List<T> set)
        {
            string sb = "";

            // Do some modifications on sb
            foreach (var item in set)
            {
                sb += "\n" + item.ToStringProperty();
            }
            return sb;
        }
        #endregion
        //------------------------------------Location------------------------------------
        #region check correctness of the location. inside israel teritory
        public static void checkLongitudeLatitude(this Location location)
        {
            if (location.Longitude > 33.2896 || location.Longitude < 29.4942)
                throw new BO.InvalidInputExeption("longitue wronge");
            if (location.Lattitude > 35.8892 || location.Lattitude < 34.2149)
                throw new BO.InvalidInputExeption("Lattitude wronge");

        }
        #endregion
        #region distance between 2 locations
        static public double distanceLongitudeLatitude(this Location first, Location second)
        {
            //check the math..
            double distance = Math.Abs(Math.Sqrt(Math.Pow(first.Longitude - second.Longitude, 2) + Math.Pow(first.Lattitude - second.Lattitude, 2)));
            return distance;
        }
        #endregion
        #region find city
        static public string findCity(this Location location)
        {
            if (location.Lattitude >= 32.9273 && location.Lattitude <= 32.9891
            && location.Longitude >= 35.4549 && location.Longitude <= 35.5394)
                return "Zefat";

            if (location.Lattitude >= 32.757674 && location.Lattitude <= 32.849
            && location.Longitude >= 34.954379 && location.Longitude <= 35.0794704)
                return "Hifa";

            if (location.Lattitude >= 32.05162 && location.Lattitude <= 32.05695
            && location.Longitude >= 34.7488269 && location.Longitude <= 34.75689)
                return "Yafo";

            if (location.Lattitude >= 31.754365 && location.Lattitude <= 31.862342
            && location.Longitude >= 34.614113 && location.Longitude <= 34.702311)
                return "Ashdod";

            if (location.Lattitude >= 31.7725 && location.Lattitude <= 31.7838
            && location.Longitude >= 35.224 && location.Longitude <= 35.239)
                return "העיר העתיקה";

            if (location.Lattitude >= 31.7082 && location.Lattitude <= 31.8830
            && location.Longitude >= 35.1252 && location.Longitude <= 35.2642)
                return "Jerusalem";

            if (location.Lattitude >= 32.056227 && location.Lattitude <= 32.1027879
            && location.Longitude >= 34.75948 && location.Longitude <= 34.8007048)
                return "Tel Aviv";

            if (location.Lattitude >= 31.19177 && location.Lattitude <= 31.32325
                        && location.Longitude >= 34.73435 && location.Longitude <= 34.85531)
                return "Beher Sheva";

            //if else
            if (location.Lattitude >= 32.98 && location.Longitude >= 35.35)
                return "Etzba Agalil";

            if (location.Lattitude >= 32.71 && location.Longitude >= 35.35)
                return "High Galil";

            if (location.Lattitude >= 32.71)
                return "גליל מערבי";

            if (location.Lattitude >= 32.41 && location.Longitude >= 35.30)
                return "עמק Beit Shean";

            if (location.Lattitude >= 32.435)
                return "Karmel";

            if (location.Lattitude >= 32.10 && location.Longitude >= 35.04)
                return "צפון השומרון";

            if (location.Lattitude >= 32.16)
                return "Netanya";

            if (location.Lattitude >= 31.85 && location.Longitude >= 35.22)
                return "Gush Dan";

            if (location.Lattitude >= 31.87 && location.Longitude >= 35.05)
                return "דרום השומרון";

            if (location.Lattitude >= 31.59 && location.Longitude >= 35.03)
                return "הרי ירושלים";

            if (location.Lattitude >= 31.59 && location.Longitude >= 34.55)
                return "השפלה";

            if (location.Lattitude >= 31.36 && location.Longitude >= 34.90)
                return "הר חברון";

            if (location.Lattitude >= 31.22 && location.Longitude >= 34.79)
                return "מערב הנגב";

            if (location.Lattitude < 30.455)
                return "דרום הנגב";

            if (location.Lattitude < 31.421)
                return "דרום הנגב";


            return "The location not found..";

        }
        #endregion
        #region compare locations
        public static bool IsEquel(this Location firstLoc, Location secondLoc)
        {
            firstLoc.checkLongitudeLatitude();      //validate first location
            secondLoc.checkLongitudeLatitude();     //validate second location
                                                    //check the coordinates:
            return (firstLoc.Lattitude == secondLoc.Lattitude) && (firstLoc.Longitude == secondLoc.Longitude);
        }
        #endregion
        #region copyLocation
        public static Location CopyLocation(this Location location)
        {
            return new Location() { Lattitude = location.Lattitude, Longitude = location.Longitude };
        }
            #endregion
            //------------------------------------input-check---------------------------------
            #region check phone number
            public static void checkPhone(this int phone)
        {
            if (phone < 100000000 || phone > 999999999)
                throw new BO.InvalidInputExeption("wrong phone");
        }
        #endregion
        #region check id
        public static void checkID(this int id)
        {
            if (id < 100000000 || id > 999999999)
                throw new BO.InvalidInputExeption("wrong id");
        }
        #endregion
        #region check charge slots
        public static void checkChargeSlote(this int chargeSlots)
        {
            if (chargeSlots < 0)
                throw new BO.InvalidInputExeption("wrong chargeSlote");
        }
        #endregion

        //-------------------------------------operator overloading-----------------------
    }
}
