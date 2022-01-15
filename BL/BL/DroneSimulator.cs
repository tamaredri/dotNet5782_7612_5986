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
                        //אם הבטריה מספיקה
                        lock (BL)
                        {
                            try
                            {
                                BL.PairDroneParcel(drone.ID);
                            }
                            catch (BattaryExeption)
                            {
                                BL.SendToCharge(drone.ID);
                            }
                            catch (DoesntExistExeption)
                            {
                                if (drone.Battery < 100)
                                    BL.SendToCharge(drone.ID);
                                else
                                    Thread.Sleep(TimeSleep * 2);
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
                            Thread.Sleep(TimeSleep / 2);
                        }
                        break;
                    case DroneStatuses.delivery:
                        lock (BL)
                        {
                            if (drone.ParcelInDeliveryByDrone != null && !drone.ParcelInDeliveryByDrone.InDelivery)
                            {
                                BL.PickUpParcelByDrone(drone.ID);
                                Thread.Sleep((int)drone.ParcelInDeliveryByDrone.Distance * ValocityDrone);

                            }
                            else if (drone.ParcelInDeliveryByDrone != null && drone.ParcelInDeliveryByDrone.InDelivery)
                            {
                                BL.DeliverParcel(drone.ID);
                                Thread.Sleep(TimeSleep);
                            }
                            Thread.Sleep(TimeSleep * 2);
                        }
                        break;
                    default:
                        break;
                }
                UpdatePresentation();
                drone = BL.GetDrone(droneID);
            }
        }
    }
}
