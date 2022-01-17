using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BO;
using static BL.BL;


namespace BL
{
    internal class DroneSimulator
    {
        BL BL;
        bool finushSimulator = false;
        public const int TimeSleep = 1000;
        const int ValocityDrone = 200;
        public DroneSimulator(BL BLAccess, int droneID, Action UpdatePresentation, Func<bool> CancllationCheck)
        {
            BL = BLAccess;


            Drone drone = BL.GetDrone(droneID);

            while (!CancllationCheck())
            {
                switch (drone.Status)
                {
                    case DroneStatuses.available:
                        //enough battery

                        try
                        {
                            lock (BL)
                            {
                                BL.PairDroneParcel(drone.ID);
                            }
                            Thread.Sleep(TimeSleep);
                        }
                        catch (BattaryExeption)
                        {
                            lock (BL)
                            {
                                BL.SendToCharge(drone.ID);
                            }
                        }
                        catch (DoesntExistExeption)
                        {
                            if (drone.Battery < 100)
                                BL.SendToCharge(drone.ID);
                            else
                            {
                                //there isnt parsel to schedule
                                finushSimulator = true;
                            }
                        }

                        Thread.Sleep(TimeSleep);
                        break;
                    case DroneStatuses.maintenance:
                        lock (BL)
                        {
                            if (drone.Battery >= 100)
                            {
                                BL.ReleaseFromCharge(droneID);
                            }
                            else
                            {
                                BL.updateBatteryThred(drone.ID);
                            }
                        }
                        break;
                    case DroneStatuses.delivery:
                        lock (BL)
                        {
                            if (drone.ParcelInDeliveryByDrone != null && !drone.ParcelInDeliveryByDrone.InDelivery)
                            {
                                BL.PickUpParcelByDrone(drone.ID);
                                //Thread.Sleep((int)drone.ParcelInDeliveryByDrone.Distance * ValocityDrone);

                            }
                            else if (drone.ParcelInDeliveryByDrone != null && drone.ParcelInDeliveryByDrone.InDelivery)
                            {
                                BL.DeliverParcel(drone.ID);
                                Thread.Sleep(TimeSleep * 2);
                            }
                        }
                        break;
                    default:
                        break;
                }
                UpdatePresentation();
                Thread.Sleep(TimeSleep * 2);
                drone = BL.GetDrone(droneID);
                if (finushSimulator == true) { break; }
            }
        }
    }
}
