using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BO;
using DO;



namespace BL
{
    public partial class BL
    {

        //------------------------Customer------------------------
        #region CreateCustomer
        public void CreateCustomer(BO.Customer customerToCreate)
        {
            //check data
            customerToCreate.ID.checkID();
            customerToCreate.Phone.checkPhone();
            customerToCreate.LocationOfCustomer.checkLongitudeLatitude();

            //BO customer-> DO customer
            DO.Customer doCustomerToCreate = new()
            {
                ID = customerToCreate.ID,
                Lattitude = customerToCreate.LocationOfCustomer.Lattitude,
                Longitude = customerToCreate.LocationOfCustomer.Longitude,
                Name = customerToCreate.Name,
                Phone = "0" + Convert.ToString(customerToCreate.Phone)
            };

            try
            { DalAccess.CreateCustomer(doCustomerToCreate); }
            catch (DO.DoesntExistExeption x)
            { throw new BO.DoesntExistExeption(x.Message, x); }
        }
        #endregion

        #region GetCustomer
        public BO.Customer GetCustomer(int customerID)
        {

            DO.Customer doCustomerToShow = new();
            try { doCustomerToShow = DalAccess.GetCustomer(customerID); }
            catch (DO.DoesntExistExeption x) { throw new BO.DoesntExistExeption(x.Message, x); }

            //create a new customer to return
            return new BO.Customer()
            {
                ID = doCustomerToShow.ID,
                LocationOfCustomer = new Location { Lattitude = doCustomerToShow.Lattitude, Longitude = doCustomerToShow.Longitude },
                Name = doCustomerToShow.Name,
                Phone = Convert.ToInt32(doCustomerToShow.Phone),
                Recieved = GetListRecivedOrSentParcels(x => x.TargetID == doCustomerToShow.ID, "SenderID").ToList(),
                Sent = GetListRecivedOrSentParcels(x => x.SenderID == doCustomerToShow.ID, "TargetID").ToList()
            };
        }
        #endregion

        #region GetCustomerList
        public IEnumerable<BO.CustomerToList> GetCustomerList() =>
                                (from customer in DalAccess.GetCustomersList()
                                 let customerFromBl = GetCustomer(customer.ID)
                                 select new CustomerToList()
                                 {
                                     ID = customerFromBl.ID,
                                     Name = customerFromBl.Name,
                                     Phone = customerFromBl.Phone,
                                     //count of parcel that were sent by the customer but not delivered
                                     //(assuming that if the parcel is in the sent list, then it's status is not created)
                                     SentParcels = customerFromBl.Sent.FindAll(x => x.Status != ParcelStatuse.pairedToDrone).Count(),
                                     //count of parcel that were received by the customer
                                     ReceivedParcels = customerFromBl.Recieved.FindAll(x => x.Status == ParcelStatuse.delivered).Count(),
                                     //count of parcel that are on the way to this customer
                                     //(assuming that if the parcel is in the recived list, then it's status is not created)
                                     OnTheWay = customerFromBl.Recieved.FindAll(x => x.Status != ParcelStatuse.delivered).Count(),
                                     //count of parcel that were sent by the customer and also delivered
                                     DeliveredParcels = customerFromBl.Sent.FindAll(x => x.Status == ParcelStatuse.delivered).Count(),

                                 }).ToList();
        #endregion

        #region GetPartOfCustomer
        public IEnumerable<CustomerToList> GetPartOfCustomer(Predicate<CustomerToList> check) =>
                                (from customer in GetCustomerList()
                                 where check(customer)
                                 select customer).ToList();
        #endregion

        #region GetCustomerForParcel
        /// <summary>
        /// get the other side of the delivery sender/target
        /// </summary>
        /// <exception cref="IBL.BO.ContradictoryDataExeption"></exception>
        /// <param name="parcel"></param>
        /// <param name="endCustomer"></param>
        /// <returns>CustomerForParcel entity with the ID and the name of the other customer</returns>
        private CustomerForParcel GetCustomerForParcel(DO.Parcel parcel, string endCustomer)
        {
            try
            {
                //chek if endCustomer is target or sender
                if (endCustomer == "TargetID")
                {
                    return new CustomerForParcel()
                    {
                        ID = parcel.TargetID,
                        Name = DalAccess.GetCustomer(parcel.TargetID).Name
                    };
                }
                else
                {
                    return new CustomerForParcel()
                    {
                        ID = parcel.SenderID,
                        Name = DalAccess.GetCustomer(parcel.SenderID).Name
                    };
                }
            }
            catch (DO.DoesntExistExeption x)
            { throw new BO.ContradictoryDataExeption("the customer doesnt exist and he the endCustomer!!", x); }
        }
        #endregion

        #region UpdateCustomer
        public void UpdateCustomer(int customreID, int newPhone, string newName)
        {
            try
            {
                //find the customer to update
                DO.Customer customerToUpdate = DalAccess.GetCustomer(customreID);

                //if one of the details is correct -> then the user wanted to update at least one detail -> send to update
                if (newPhone != 0 || (newName is not null && newName is not ""))
                {
                    if (newPhone != 0)
                    {
                        try { newPhone.checkPhone(); }
                        catch (BO.InvalidInputExeption) { newPhone = -1; }
                    }

                    DalAccess.UpdateCustomer(customreID, newPhone, newName);
                    if (newPhone == -1) throw new BO.InvalidInputExeption("the new phone is in incorrect format");
                }
            }
            catch (DO.DoesntExistExeption e)
            { throw new BO.DoesntExistExeption(e.Message, e); }
        }
        #endregion
    }
}
