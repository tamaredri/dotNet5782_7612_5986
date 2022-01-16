using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BL
{
    public partial class BL
    {
        //------------------------Station------------------------
        #region CreateStation
        public void CreateStation(BO.Station stationToCreate)
        {
            //chek data
            stationToCreate.StationLocation.checkLongitudeLatitude();
            stationToCreate.AvailableChargeSlots.checkChargeSlote();
           
            //BO station-> DO station
            DO.Station doStationToCreate = new()
            {
                ID = stationToCreate.ID,
                Lattitude = stationToCreate.StationLocation.Lattitude,
                Longitude = stationToCreate.StationLocation.Longitude,
                Name = stationToCreate.Name,
                ChargeSlots = stationToCreate.AvailableChargeSlots
                
            };

            try
            { DalAccess.CreateStation(doStationToCreate); }
            catch(DO.AlreadyExistExeption x)
            { throw new BO.AlreadyExistExeption(x.Message, x); }
        }
        #endregion

        #region GetStation
        public BO.Station GetStation(int stationID)
        {
            DO.Station doStationToShow = new();
            try { doStationToShow = DalAccess.GetStation(stationID); }
            catch(DO.DoesntExistExeption x) { throw new BO.DoesntExistExeption(x.Message, x); }

            //create a BL station
            BO.Station boStationToShow = new()
            {
                ID = doStationToShow.ID,
                AvailableChargeSlots = doStationToShow.ChargeSlots/*chargeslote= avalable chargeslote*/,
                Name = doStationToShow.Name,
                StationLocation = new() { Lattitude = doStationToShow.Lattitude, Longitude = doStationToShow.Longitude }
            };
            
            //create a list of all the drones that are charging in the station
            boStationToShow.ChargedDrones = GetDroneInCharge(doStationToShow.ID).ToList();

            return boStationToShow;
        }
        #endregion

        #region GetStationList
        public IEnumerable<StationToList> GetStationList()
        {
            List<DO.Station> doStationList = DalAccess.GetStationList().ToList();

            return (from station in doStationList
                    let stationFromBl = DalAccess.GetStation(station.ID)
                    select new StationToList()
                    {
                        ID = stationFromBl.ID,
                        Name = stationFromBl.Name,
                        AvailableChargeSlots = stationFromBl.ChargeSlots,
                        UsedChargeSlots = DalAccess.GetPartOfDroneCharge(x =>
                     (x.Stationld == stationFromBl.ID) && (x.IsInCharge == true))
                    .Count()
                    }).ToList();
        }
        #endregion

        #region GetPartOfStation
        public IEnumerable<StationToList> GetPartOfStation(Predicate<StationToList> check)
        {
            var query = (from station in GetStationList()
                         where check(station)
                         select station).ToList();
            return query;
        }
        #endregion

        #region UpdateStation
        public void UpdateStation(int stationID, int newChargeSlots=0, string newName = "")
        {
                //find the station to update
                DO.Station stationToUpdate = DalAccess.GetStation(stationID);
            try
            {
                //if one of the details is correct -> then the user wanted to update at least one detail -> send to update
                if (newChargeSlots > 0 || newName != "")
                {
                    /*
                     *find the amount of drones that are charging in the station that is wanted for update
                     *if the new charge slots are not enough for the drones that are charging
                     *-> throw an exception ?
                     *-> or update the other details anyway (reseting the newChrge value to default)?
                     */ //                                                 the station location
                    if (newChargeSlots > 0)
                    {
                        if (newChargeSlots - amountOfDronesInChargeInTheStation(new Location()
                        {
                            Lattitude = stationToUpdate.Lattitude,
                            Longitude = stationToUpdate.Longitude
                        }) < 0)
                            newChargeSlots = -1;
                    }

                    DalAccess.UpdateStation(stationID, newChargeSlots, newName);
                    if (newChargeSlots == -1)
                        throw new BO.InvalidInputExeption("there are more drone in charge then the new number of charge slote");
                    

                }
            }
            catch(DO.DoesntExistExeption x)
            {throw new BO.DoesntExistExeption(x.Message, x);}
        }
        #endregion

        #region findClosestStation
        /// <summary>
        /// return the id of the closest station to a specific location and check if it applies a codition
        /// </summary>
        /// <remarks>the chose predicate will check oly the closest station, not for each station</remarks>
        /// <exception cref="DoesntExistExeption"></exception>
        /// <param name="locationtoFindTheClosest"></param>
        /// <returns>the id of the closest station location the aplly the codition</returns>
        int findClosestStation(Location locationToCompare, Predicate<DO.Station> chose)
        {
            //all the stations
            List<DO.Station> allTheStations = (List<DO.Station>)DalAccess.GetStationList();
            int closestStatioID = 0;

            //the first station to compare to
            Location closestLocation = new() { Lattitude = 21.837778, Longitude = 39.329861 };
            bool isItOk = true;
            //find a closer station
            foreach (var station in allTheStations)
            {
                //find the closest station. in the first iteration -> the closest station contains the first station's location
                if (locationToCompare.DistanceBetweenPlaces(closestLocation)
                    >
                    locationToCompare.DistanceBetweenPlaces(new Location()
                    {
                        Lattitude = station.Lattitude,
                        Longitude = station.Longitude
                    }))
                {
                    //update the closest location + the station ID
                    closestStatioID = station.ID;
                    closestLocation.Lattitude = station.Lattitude;
                    closestLocation.Longitude = station.Longitude;
                    if (!chose(DalAccess.GetStation(closestStatioID)))
                    { isItOk = false; }
                    else { isItOk = true; }

                }
                else
                {
                    if (locationToCompare.DistanceBetweenPlaces(closestLocation)
                  ==
                  locationToCompare.DistanceBetweenPlaces(new Location()
                  {
                      Lattitude = station.Lattitude,
                      Longitude = station.Longitude
                  }))
                    {   //if check(closeLocation)== false
                        if (isItOk==false/*!chose(DalAccess.GetStation(closestStatioID))*/)
                        { //update the closest location + the station ID
                            closestStatioID = station.ID;
                            closestLocation.Lattitude = station.Lattitude;
                            closestLocation.Longitude = station.Longitude;
                            if (!chose(DalAccess.GetStation(closestStatioID)))
                            { isItOk = false; }
                            else { isItOk = true; }
                        }
                    } 
                }
            }

            //if the closest station doesn't apply the condition -> throw an exception
            if (isItOk == false) {
                UpdateStation(closestStatioID, amountOfDronesInChargeInTheStation(GetStation(closestStatioID).StationLocation)+5);
            }
            //if the closest station applies the codition -> return the location
            return closestStatioID;
        }
        #endregion

        #region delete
        #endregion
    }
}
