using System;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.IO;

namespace Dal
{
    public struct ImportentNumbers
    {
        public int numberSaved { get; set; }
        public string typeOfnumber { get; set; }
    }

    sealed class DalXml : IDal
    {
        #region singlton
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml()
        {
            //List<ImportentNumbers> helpList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);
            //ImportentNumbers newImp = helpList.Find(x => x.typeOfnumber == "Parcel Running Number");
            //helpList.Remove(newImp);

            //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Drone Running Number" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Station Running Number" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Parcel Running Number" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Charge Running Number" });

            //helpList.Add(new ImportentNumbers() { numberSaved = 10, typeOfnumber = "Minimum If Available" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 20, typeOfnumber = "Minimum If Carry Light Weigh" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 30, typeOfnumber = "Minimum If Carry Middle Weight" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 40, typeOfnumber = "Minimum If Carry Heavy Weight" });
            //helpList.Add(new ImportentNumbers() { numberSaved = 50, typeOfnumber = "Charging Precentage Per Hour" });

            //newImp.numberSaved = 0;
            //helpList.Add(newImp);
            //XmlTools.SaveListToXMLSerializer<ImportentNumbers>(helpList, configPath);
            ////initialize
            //Random rand = new Random();
            #region initializing customer list:
            ////first customer
            //CreateCustomer(new Customer()
            //{
            //    ID = 322637596,
            //    Name = "Tamar",
            //    Phone = "0" + rand.Next(0580000000, 0590000000),
            //    Lattitude = 31.713620,
            //    Longitude = 34.997140 //Beit Shemesh
            //});

            ////second customer
            //CreateCustomer(new Customer()
            //{
            //    ID = 323805695,
            //    Name = "Reut",
            //    Phone = "0" + rand.Next(0580000000, 0590000000),
            //    Lattitude = 32.982125,
            //    Longitude = 35.810217 //jerusalem

            //});

            ////third customer
            //CreateCustomer(new Customer()
            //{
            //    ID = 225658541,
            //    Name = "Avraham",
            //    Phone = "0" + rand.Next(0580000000, 0590000000),
            //    Lattitude = 32.795501,
            //    Longitude = 35.531364 //tveria
            //});

            ////forth customer
            //CreateCustomer(new Customer()
            //{
            //    ID = 033584722,
            //    Name = "Moshe",
            //    Phone = "0" + rand.Next(0580000000, 0590000000),
            //    Lattitude = 31.604789,
            //    Longitude = 34.767405 //kiryat gat
            //});

            ////fifth customer
            //CreateCustomer(new Customer()
            //{
            //    ID = 221156457,
            //    Name = "Ayala",
            //    Phone = "0" + rand.Next(0580000000, 0590000000),
            //    Lattitude = 31.560358,
            //    Longitude = 34.843347 //lachish
            //});
            #endregion

            #region initializing base station list:

            ////first station
            //CreateStation(new Station
            //{
            //    ChargeSlots = 3,
            //    Lattitude = 31.767827, //jerusalem
            //    Longitude = 35.177496,
            //    Name = "Drones - Kiryat Yovel"
            //});

            ////second station
            //CreateStation(new Station
            //{
            //    ChargeSlots = 3,
            //    Lattitude = 31.799753,
            //    Longitude = 34.646484, //ashdod
            //    Name = "Ashdo-D-rones"
            //});

            #endregion

            #region initializing drones list:

            //CreateDrone(new Drone
            //{
            //    MaxWeight = WeightCategories.heavey,
            //    Model = "Ka233"
            //});

            //CreateDrone(new Drone
            //{
            //    MaxWeight = WeightCategories.light,
            //    Model = "Ka998"
            //});

            //CreateDrone(new Drone
            //{
            //    MaxWeight = WeightCategories.middle,
            //    Model = "j-123"
            //});

            //CreateDrone(new Drone
            //{
            //    MaxWeight = WeightCategories.heavey,
            //    Model = "xp-666"
            //});

            //CreateDrone(new Drone
            //{
            //    MaxWeight = WeightCategories.middle,
            //    Model = "dr258"
            //});
            #endregion

            #region initializing parcel list:

            //CreateParcel(new Parcel
            //{
            //    SenderID = 322637596 ,
            //    TargetID = 323805695 ,
            //    Weight = WeightCategories.light,
            //    Priority = Prioritie.emergency,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 225658541,
            //    TargetID = 221156457,
            //    Weight = WeightCategories.light,
            //    Priority = Prioritie.fast,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 221156457,
            //    TargetID = 225658541,
            //    Weight = WeightCategories.light,
            //    Priority = Prioritie.regular,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 221156457,
            //    TargetID = 322637596,
            //    Weight = WeightCategories.heavey,
            //    Priority = Prioritie.regular,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 323805695,
            //    TargetID = 221156457,
            //    Weight = WeightCategories.middle,
            //    Priority = Prioritie.regular,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 033584722,
            //    TargetID = 225658541,
            //    Weight = WeightCategories.middle,
            //    Priority = Prioritie.fast,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 225658541,
            //    TargetID = 323805695,
            //    Weight = WeightCategories.middle,
            //    Priority = Prioritie.regular,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 033584722,
            //    TargetID = 221156457,
            //    Weight = WeightCategories.light,
            //    Priority = Prioritie.fast,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 225658541,
            //    TargetID = 322637596,
            //    Weight = WeightCategories.heavey,
            //    Priority = Prioritie.fast,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            //CreateParcel(new Parcel
            //{
            //    SenderID = 221156457,
            //    TargetID = 322637596,
            //    Weight = WeightCategories.light,
            //    Priority = Prioritie.fast,
            //    Requested = DateTime.Now,
            //    DroneID = 0
            //});

            #endregion
        }
        static DalXml() { }
        #endregion

        #region DS xml file

        string dronePath = @"droneXml.xml";
        string droneChargePath = @"droneChargeXml.xml";
        string customerPath = @"customerXml.xml";
        string parcelPath = @"parcelXml.xml";
        string successfullyParcelPath = @"successfullyParcelXml.xml";
        string stationPath = @"stationXml.xml";
        string configPath = @"config.xml";

        #endregion

        //-----------------customer--------------
        #region customer XElement

        #region AddCustomer
        public void CreateCustomer(Customer customerToCreate)
        {
            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath); //get all the elements from the file

            //check if the customer exists in th file
            var customerFromFile = (from customer in customerRoot.Elements()
                                    where (customer.Element("ID").Value == customerToCreate.ID.ToString())
                                    select customer).FirstOrDefault();

            //throw an exception
            if (customerFromFile != null)
                throw new AlreadyExistExeption("the customer already exit");

            //add the customer to the root element
            customerRoot.Add(
                new XElement("customer",
                new XElement("ID", customerToCreate.ID),
                new XElement("Name", customerToCreate.Name),
                new XElement("Phone", customerToCreate.Phone),
                new XElement("Lattitude", customerToCreate.Lattitude),
                new XElement("Longitude", customerToCreate.Longitude)));

            //save the root in the file
            XmlTools.SaveListToXMLElement(customerRoot, customerPath);
        }
        #endregion

        #region GetCustomer
        public Customer GetCustomer(int idToGet)
        {
            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath); //get all the elements from the file

            //check if the customer exists in th file
            var customerFromFile = (from customer in customerRoot.Elements()
                                    where (customer.Element("ID").Value == idToGet.ToString())
                                    select customer).FirstOrDefault();

            //throw an exception
            if (customerFromFile == null)
                throw new DoesntExistExeption("the customer Doesnt exit");

            return new Customer()
            {
                ID = int.Parse(customerFromFile.Element("ID").Value),
                Name = customerFromFile.Element("Name").Value,
                Phone = customerFromFile.Element("Name").Value,
                Longitude = int.Parse(customerFromFile.Element("Longitude").Value),
                Lattitude = int.Parse(customerFromFile.Element("Lattitude").Value),
            };
        }
        #endregion

        #region GetCustomersList
        public IEnumerable<Customer> GetCustomersList()
        {
            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath); //get all the elements from the file

            return (from customer in customerRoot.Elements()
                    select new Customer()
                    {
                        ID = int.Parse(customer.Element("ID").Value),
                        Name = customer.Element("Name").Value,
                        Phone = customer.Element("Name").Value,
                        Longitude = int.Parse(customer.Element("Longitude").Value),
                        Lattitude = int.Parse(customer.Element("Lattitude").Value),
                    }).ToList();
        }
        #endregion

        #region GetPartOfCustomer
        public IEnumerable<Customer> GetPartOfCustomer(Predicate<Customer> check)
        {
            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath); //get all the elements from the file

            return (from customer in customerRoot.Elements()
                    let customerToCheck = new Customer()
                    {
                        ID = int.Parse(customer.Element("ID").Value),
                        Name = customer.Element("Name").Value,
                        Phone = customer.Element("Name").Value,
                        Longitude = int.Parse(customer.Element("Longitude").Value),
                        Lattitude = int.Parse(customer.Element("Lattitude").Value),
                    }
                    where check(customerToCheck)
                    select customerToCheck).ToList();
        }
        #endregion

        #region UpdateCustomer
        public void UpdateCustomer(int customerID, int newPhone = 0, string newName = null)
        {
            XElement customerRoot = XmlTools.LoadListFromXMLElement(customerPath); //get all the elements from the file

            //check if the customer exists in th file
            var customerFromFile = (from customer in customerRoot.Elements()
                                    where (customer.Element("ID").Value == customerID.ToString())
                                    select customer).FirstOrDefault();

            //throw an exception
            if (customerFromFile == null)
                throw new DoesntExistExeption("the customer doesnt exit");

            Customer customerToUpdate = GetCustomer(customerID);

            if (newPhone >= 100000000 || newPhone <= 999999999)
                customerFromFile.Element("Phone").Value = "0" + newPhone;
            if (newName != null)
                customerFromFile.Element("Name").Value = newName;
        }
        #endregion

        #region DeleteCustomer
        #endregion

        #endregion

        //-----------------drone-----------------
        #region drone XmlSerializer
        #region CreateDrone
        public int CreateDrone(Drone droneToCreate)
        {
            List<Drone> dronesList = GetDroneList().ToList();
            List<ImportentNumbers> runningList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Drone Running Number")
                                           select number).FirstOrDefault();

            runningList.Remove(runningNum);

            runningNum.numberSaved++;
            droneToCreate.ID = runningNum.numberSaved;

            runningList.Add(runningNum);
            dronesList.Add(droneToCreate);

            XmlTools.SaveListToXMLSerializer(runningList, configPath);
            XmlTools.SaveListToXMLSerializer(dronesList, dronePath);

            return runningNum.numberSaved;
        }
        #endregion

        #region GetDrone
        public Drone GetDrone(int idToGet)
        {
            List<Drone> dronesList = GetDroneList().ToList();

            Drone droneToReturn = (from drone in dronesList
                                   where drone.ID == idToGet
                                   select drone).FirstOrDefault();

            if (droneToReturn.Equals(default(Drone)))
                throw new DoesntExistExeption("GetDrone : the drone doesn't exist");

            return droneToReturn;
        }
        #endregion

        #region GetDroneList
        public IEnumerable<Drone> GetDroneList()
        {
            return XmlTools.LoadListFromXMLSerializer<Drone>(dronePath);

        }
        #endregion

        #region GetPartOfDrone
        public IEnumerable<Drone> GetPartOfDrone(Predicate<Drone> check)
        {
            List<Drone> dronesList = GetDroneList().ToList();

            return (from drone in dronesList
                    where check(drone)
                    select drone).ToList();
        }
        #endregion

        #region GetPowerConsumptionByDrone
        public double[] GetPowerConsumptionByDrone()
        {
            return new double[] {
            getImportentNumber("Minimum If Available"),
            getImportentNumber("Minimum If Carry Light Weigh"),
            getImportentNumber("Minimum If Carry Middle Weight"),
            getImportentNumber("Minimum If Carry Heavy Weight"),
            getImportentNumber("Charging Precentage Per Hour"),
            };
        }
        #endregion

        #region GetDroneRunningNumber
        public int GetDroneRunningNumber()
        {
            return (int)getImportentNumber("Drone Running Number");//return DataSource.Config.runningDroneNumber; 
        }
        #endregion

        #region UpdateDrone
        public void UpdateDrone(int id, string newModel)
        {
            Drone droneToUpdate = GetDrone(id);

            List<Drone> dronesList = GetDroneList().ToList();

            dronesList.Remove(droneToUpdate);
            droneToUpdate.Model = newModel;
            dronesList.Add(droneToUpdate);

            XmlTools.SaveListToXMLSerializer(dronesList, dronePath);
        }
        #endregion

        #region SendToCharge
        public void SendToCharge(int droneIDToCharge, int stationIDToCharge)
        {
            Drone droneToCharge = GetDrone(droneIDToCharge);

            //send to charge in the station.
            updateChargeSlots(stationIDToCharge,
                                x =>
                                {
                                    if (x <= 0)
                                        throw new DoesntExistExeption("there are no charge slots available in the station");
                                    return --x;
                                });

            createChargeEntity(droneIDToCharge, stationIDToCharge);
        }
        #endregion

        #region ReleaseFromCharge
        public void ReleaseFromCharge(int droneIDToRelease)
        {
            GetDrone(droneIDToRelease);//doesnt exist exception 
            deleteChargeEntity(droneIDToRelease); //release
        }
        #endregion

        #region DeleteDrone
        #endregion

        #region private drone functions
        #region get a runnig/consumption number
        double getImportentNumber(string typeOfNumber)
        {
            List<ImportentNumbers> runningList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == typeOfNumber)
                                           select number).FirstOrDefault();
            if (runningNum.Equals(default(ImportentNumbers)))
                throw new DoesntExistExeption();

            return runningNum.numberSaved;
        }
        #endregion
        #endregion

        #endregion

        //-----------------drone-charge-----------
        #region droneCharge XmlSerializer

        #region createChargeEntity
        /// <summary>
        /// create a new drone-charge entity
        /// </summary>
        /// <remarks>assuming the parameters are correct. 
        /// the function is private, so only class functions can use it</remarks>
        /// <param name="droneIDToCharge"></param>
        /// <param name="stationIDToCharge"></param>
        void createChargeEntity(int droneIDToCharge, int stationIDToCharge)
        {
            List<DroneCharge> chargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);
            List<ImportentNumbers> runningList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Charge Running Number")
                                           select number).FirstOrDefault();

            runningList.Remove(runningNum);

            runningNum.numberSaved++;

            chargeList.Add(new DroneCharge()
            {
                ID = runningNum.numberSaved,
                Droneld = droneIDToCharge,
                IsInCharge = true,
                Stationld = stationIDToCharge,
                TimeStart = DateTime.Now
            });
            runningList.Add(runningNum);

            XmlTools.SaveListToXMLSerializer(runningList, configPath);
            XmlTools.SaveListToXMLSerializer(chargeList, droneChargePath);
        }
        #endregion

        #region GetDroneCharge

        #endregion

        #region deleteChargeEntity
        /// <summary>
        /// the function removes a drone-charge entity
        /// </summary>
        /// <exception cref="DoesntExistExeption">if the drone isn't charging</exception>
        /// <param name="droneID"></param>
        void deleteChargeEntity(int droneID)
        {
            List<DroneCharge> chargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

            DroneCharge chargeToUpdate = (from droneCharge in chargeList
                                          where droneCharge.Droneld == droneID
                                          select droneCharge).FirstOrDefault();

            if (chargeToUpdate.ID.Equals(default(DroneCharge)))
                throw new DoesntExistExeption("the drone is not charging");

            //update in the station that the drone is released from charge
            updateChargeSlots(chargeToUpdate.Stationld, x => ++x);

            chargeList.Remove(chargeToUpdate);
            chargeToUpdate.IsInCharge = false;
            chargeToUpdate.TimeFinish = DateTime.Now;
            chargeList.Add(chargeToUpdate);

            XmlTools.SaveListToXMLSerializer(chargeList, droneChargePath);
        }
        #endregion

        #region GetPartOfDroneCharge
        public IEnumerable<DroneCharge> GetPartOfDroneCharge(Predicate<DroneCharge> check)
        {
            List<DroneCharge> chargeList = XmlTools.LoadListFromXMLSerializer<DroneCharge>(droneChargePath);

            return (from droneCharge in chargeList
                    where check(droneCharge)
                    select droneCharge).ToList();
        }
        #endregion

        #region private drone-charge functions
        #region getAmountOfUsedChargeSlots
        /// <summary>
        /// get the amount of the charging slots taken in a specific station
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        int getAmountOfUsedChargeSlots(int stationID)
        {
            GetStation(stationID);

            return (from charge in GetPartOfDroneCharge(x => x.IsInCharge == true)
                    where charge.ID == stationID
                    select charge).ToList().Count();
        }
        #endregion
        #endregion

        #endregion

        //-----------------parcel-----------------
        #region parcel XmlSerializer

        #region CreateParcel
        public void CreateParcel(Parcel parcelToCreate)
        {
            List<Parcel> parcelsList = GetParcelList().ToList();
            List<ImportentNumbers> runningList =XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Parcel Running Number")
                                           select number).FirstOrDefault();
            runningList.Remove(runningNum);
            runningNum.numberSaved++;
            runningList.Add(runningNum);
            parcelToCreate.ID = runningNum.numberSaved;
            parcelsList.Add(parcelToCreate);

            XmlTools.SaveListToXMLSerializer(parcelsList, parcelPath);
            XmlTools.SaveListToXMLSerializer(runningList, configPath);

        }
        #endregion

        #region GetParcel
        public Parcel GetParcel(int idToGet)
        {
            List<Parcel> parcelsList = GetParcelList().ToList();
            List<Parcel> SuccessfullyParcelList = XmlTools.LoadListFromXMLSerializer<Parcel>(successfullyParcelPath);

            Parcel parcelToGet = (from parcel in parcelsList
                                  where parcel.ID == idToGet
                                  select parcel).FirstOrDefault();

            if (parcelToGet.Equals(default(Parcel)))
            {   
                //the parcel is not in the existing parcels, look for it in the delivered parcels
                parcelToGet = (from parcel in SuccessfullyParcelList
                               where parcel.ID == idToGet
                               select parcel).FirstOrDefault();

                if (parcelToGet.Equals(default(Parcel)))
                    throw new DoesntExistExeption("the parcel dosen't exited");
            }

            return parcelToGet;
        }
        #endregion

        #region GetParcelList
        public IEnumerable<Parcel> GetParcelList()
        {
            return XmlTools.LoadListFromXMLSerializer<Parcel>(parcelPath);
        }
        #endregion

        #region GetsuccessfullyDeliveredParcelList
        public IEnumerable<Parcel> GetsuccessfullyDeliveredParcelList()
        {
            return  XmlTools.LoadListFromXMLSerializer<Parcel>(successfullyParcelPath);
        }
        #endregion

        #region GetPartOfParcel
        public IEnumerable<Parcel> GetPartOfParcel(Predicate<Parcel> check)
        {
            List<Parcel> parcelsList = GetParcelList().ToList();
            List<Parcel> SuccessfullyParcelList = GetsuccessfullyDeliveredParcelList().ToList();


            List<Parcel> parcelListToReturn= (from parcel in parcelsList
                    where check(parcel)
                    select parcel).ToList<Parcel>();

            List<Parcel> parcelListToReturn2 = (from parcel in SuccessfullyParcelList
                                               where check(parcel)
                                               select parcel).ToList<Parcel>();

            parcelListToReturn.AddRange(parcelListToReturn2);
            return parcelListToReturn;
        }
        #endregion

        #region ScheduleDroneParcel
        public void ScheduleDroneParcel(int idParcelToSchedule, int idDroneToSchedule)
        {
            List<Parcel> parcelList = GetParcelList().ToList();

            Parcel parcelToSchedule = GetParcel(idParcelToSchedule);//doesnt exist exception

            GetDrone(idDroneToSchedule);//doesnt exist exception

            parcelList.Remove(parcelToSchedule);
            parcelToSchedule.Scheduled = DateTime.Now;
            parcelToSchedule.DroneID = idDroneToSchedule;
            parcelList.Add(parcelToSchedule);

            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
        }
        #endregion

        #region PickUpByDrone
        public void PickUpByDrone(int idParcelToPickUp)
        {
            List<Parcel> parcelsList = GetParcelList().ToList();

            Parcel parcelToPickUp = GetParcel(idParcelToPickUp);

            parcelsList.Remove(parcelToPickUp);
            parcelToPickUp.PickedUp = DateTime.Now;
            parcelsList.Add(parcelToPickUp);

            XmlTools.SaveListToXMLSerializer(parcelsList, parcelPath);
        }
        #endregion

        #region DelivereParcel
        public void DelivereParcel(int idParcelToDelivere)
        {
            List<Parcel> parcelList = GetParcelList().ToList();
            List<Parcel> successfullyDeliveredParcelList = GetsuccessfullyDeliveredParcelList().ToList();
            

            Parcel parcelToDeliver = GetParcel(idParcelToDelivere);//doesnt exist exception

            parcelList.Remove(parcelToDeliver);
            parcelToDeliver.Delivered = DateTime.Now;
            successfullyDeliveredParcelList.Add(parcelToDeliver);

            XmlTools.SaveListToXMLSerializer(parcelList, parcelPath);
            XmlTools.SaveListToXMLSerializer(successfullyDeliveredParcelList, successfullyParcelPath);
        }
        #endregion

        #region DeleteParcel
        #endregion

        #endregion

        //-----------------station-----------------
        #region station XmlSerializer

        #region CreateStation
        public void CreateStation(Station stationToCreate)
        {
            List<Station> stationsList = GetStationList().ToList();
            List<ImportentNumbers> runningList = XmlTools.LoadListFromXMLSerializer<ImportentNumbers>(configPath);

            //check if there is a station with the same name and location as the wanted station to add
            if (stationsList.Exists(x => x.Name == stationToCreate.Name) ||
                stationsList.Exists(x => (x.Lattitude == stationToCreate.Lattitude)
                                                   && (x.Longitude == stationToCreate.Longitude)))
                throw new AlreadyExistExeption("the station already exist");

            ImportentNumbers runningNum = (from number in runningList
                                           where (number.typeOfnumber == "Station Running Number")
                                           select number).FirstOrDefault();

            runningList.Remove(runningNum);
            runningNum.numberSaved++;
            stationToCreate.ID = runningNum.numberSaved;

            runningList.Add(runningNum);
            stationsList.Add(stationToCreate);

            XmlTools.SaveListToXMLSerializer(runningList, configPath);
            XmlTools.SaveListToXMLSerializer(stationsList, stationPath);

        }
        #endregion

        #region GetStation
        public Station GetStation(int idToGet)
        {
            List<Station> stationsList = GetStationList().ToList();


            Station stationToGet = (from station in stationsList
                                    where 
                                    (station.ID == idToGet)
                                    select station).FirstOrDefault();

            if (stationToGet.Equals(default(Station)))
                throw new DoesntExistExeption("the station doesn't exist");
            return stationToGet;
        }
        #endregion

        #region GetStationList
        public IEnumerable<Station> GetStationList()
        {
            return XmlTools.LoadListFromXMLSerializer<Station>(stationPath);
        }
        #endregion

        #region GetPartOfStation
        public IEnumerable<Station> GetPartOfStation(Predicate<Station> check)
        {
            List<Station> stationsList = GetStationList().ToList();

            return (from station in stationsList
                    where check(station)
                    select station).ToList<Station>();

        }
        #endregion

        #region UpdateStation
        public void UpdateStation(int stationIDToUpdate, int newChargeSlots, string newName)
        {
            List<Station> stationsList = GetStationList().ToList();

            Station stationToUpdate = GetStation(stationIDToUpdate);

            if (newChargeSlots >= 0)
            {
                int usedChargeSlots = getAmountOfUsedChargeSlots(stationIDToUpdate);
                updateChargeSlots(stationIDToUpdate, x => x = newChargeSlots - usedChargeSlots); //check
                stationsList = GetStationList().ToList(); //reread the updated file
            }


            if (newName != null)
            {
                stationsList.Remove(stationToUpdate);
                stationToUpdate.Name = newName;
                stationsList.Add(stationToUpdate);
            }
            XmlTools.SaveListToXMLSerializer(stationsList, stationPath);
        }
        #endregion

        #region DeleteStation
        #endregion

        #region private station functions
        #region updateChargeSlots
        /// <summary>
        /// updating the amount of the charging slots according to an action chosen by you
        /// </summary>
        /// <remarks></remarks>
        /// <param name="stationIDToCharge"></param>
        /// <param name="update"></param>
        void updateChargeSlots(int stationIDToCharge, Converter<int, int> update)
        {
            List<Station> stationsList = GetStationList().ToList();

            Station stationToPair = GetStation(stationIDToCharge);//doesnt exist exception

            stationsList.Remove(stationToPair);
            stationToPair.ChargeSlots = update(stationToPair.ChargeSlots);
            stationsList.Add(stationToPair);

            XmlTools.SaveListToXMLSerializer(stationsList, stationPath);
        }
        #endregion
        #endregion

        #endregion

        //List<ImportentNumbers> helpList = new List<ImportentNumbers>();
        //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Drone Running Number" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Station Running Number" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Parcel Running Number" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 0, typeOfnumber = "Charge Running Number" });

        //helpList.Add(new ImportentNumbers() { numberSaved = 10, typeOfnumber = "Minimum If Available" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 20, typeOfnumber = "Minimum If Carry Light Weigh" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 30, typeOfnumber = "Minimum If Carry Middle Weight" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 40, typeOfnumber = "Minimum If Carry Heavy Weight" });
        //helpList.Add(new ImportentNumbers() { numberSaved = 50, typeOfnumber = "Charging Precentage Per Hour" });

        //XmlTools.SaveListToXMLSerializer<ImportentNumbers>(helpList, configPath);
    }
}
