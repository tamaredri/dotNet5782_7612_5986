using System;
using BO;
using BL;

namespace ConsoleBl
{
    class Program
    {
        static void Main(string[] args)
        {
            Location loc1 = new Location() { Lattitude = 35.64174312417117, Longitude = 33.2420881975982 };
            Location loc2 = new Location() { Lattitude = 34.90282992324121, Longitude = 29.5203758726536 };

            loc1.checkLongitudeLatitude();
            loc2.checkLongitudeLatitude();

            double d = loc1.DistanceBetweenPlaces(loc2);
            Console.WriteLine(d.ToString());
        }
    }
}