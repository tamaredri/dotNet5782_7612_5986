using System;
using DalApi;
using System.Xml.Linq;
using DO;

namespace Dal
{
    sealed class DalXml //: IDal
    {
        #region singlton
        //static readonly IDal instance = new DalXml();
        //public static IDal Instance { get => instance; }
        DalXml() { }
        static DalXml() { }
        #endregion

        #region DS xml file

        string dronePath = "@DroneXml.xml";
        string customerPath = "@CustomereXml.xml";
        string parcelPath = "@ParcelXml.xml";
        string stationPath = "@StationXml.xml";

        #endregion

        #region customer XElement

        //-----------------customer--------------
        #region AddCustomer

        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return XElement.Load(filePath);
                }
                else
                {
                    XElement rootElem = new XElement(filePath);
                    if (filePath == @"configurationXml.xml")
                        rootElem.Add(new XElement("BusLineID", 1));
                    rootElem.Save(filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new LoadingException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }


        public void CreateCustomer(Customer customerToCreate)
        {
            XElement customers = new XElement("customers");

            //DataSource.Config.runningCustomerNumber++;

            //if (DataSource.CustomersList.Find(x => x.ID == customerToCreate.ID).Name != null)
            //    throw new AlreadyExistExeption("the customer already exit");

            //DataSource.CustomersList.Add(customerToCreate);
        }
        #endregion
        #region GetCustomer
        public Customer GetCustomer(int idToGet)
        {
            Customer customerToGet = (from customer in DataSource.CustomersList
                                      where customer.ID == idToGet
                                      select customer).FirstOrDefault();
            if (customerToGet.Name == null)
                throw new DoesntExistExeption("the customer doesn't exited");
            return customerToGet;
        }
        #endregion
        #region GetCustomersList
        public IEnumerable<Customer> GetCustomersList()
        {
            return (from customer in DataSource.CustomersList
                    select customer).ToList();
        }
        #endregion
        #region GetPartOfCustomer
        public IEnumerable<Customer> GetPartOfCustomer(Predicate<Customer> check)
        {
            return (from customer in DataSource.CustomersList
                    where check(customer)
                    select customer).ToList<Customer>();
        }
        #endregion
        #region UpdateCustomer
        public void UpdateCustomer(int customerID, int newPhone = 0, string newName = null)
        {
            Customer customerToUpdate = GetCustomer(customerID);

            DataSource.CustomersList.Remove(customerToUpdate);
            if (newPhone >= 100000000 || newPhone <= 999999999)
                customerToUpdate.Phone = "0" + newPhone.ToString();
            if (newName != null)
                customerToUpdate.Name = newName;
            DataSource.CustomersList.Add(customerToUpdate);
        }
        #endregion
        #region DeleteCustomer
        #endregion

        #endregion
    }
}
