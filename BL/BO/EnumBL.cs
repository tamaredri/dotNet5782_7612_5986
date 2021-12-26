using System;

namespace BO
{

    public enum DroneStatuses
    { available, maintenance, delivery }

    public enum ParcelStatuse
    { created, pairedToDrone, pickedUp, delivered}

    public enum Priorities//parcel
    { regular, fast, emergency }

    public enum WeightCategories
    { light, middle, heavey }
}
