using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class CustomerLogic
    {
        public List<Customer> GetAllCustomerItems()
        {
            try
            {
                string _Query = "Select * from Customer where IsActive='true'";

                var _result = SessionData.SQLDataConnection.Query<Customer>(_Query);

                return _result;
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
            return null;
        }

        public List<Customer> SearchCustomerItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from Customer where CustomerName like '" + SearchText + "%'";

                var _result = SessionData.SQLDataConnection.Query<Customer>(_Query);

                return _result;
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
            return null;
        }

        public void InsertCustomerItem(Customer _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO Customer(CustomerName,Mobile,Email,BillingAddress,BillingCity," +
                        "BillingState,BillingZipcode,IsShippingAddressSame,ShippingAddress,ShippingCity," +
                        "ShippingState,ShippingZipcode,IsActive) VALUES('" +
                        _Item.CustomerName + "','" + _Item.Mobile + "','" + _Item.Email + "','" + _Item.BillingAddress + "','" +
                        _Item.BillingCity + "','" + _Item.BillingState + "','" + _Item.BillingZipCode + "','" + _Item.IsShippingAddressSame + "','" +
                        _Item.ShippingAddress + "','" + _Item.ShippingCity + "','" + _Item.ShippingState + "','" +
                        _Item.ShippingZipCode + "','" + _Item.IsActive + "')";

                    SessionData.SQLDataConnection.Execute(_Query);
                }
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
        }

        public void UpdateCustomerItem(Customer _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update Customer Set CustomerName='" + _Item.CustomerName + "',Mobile='" + _Item.Mobile + "',Email='" + _Item.Email + "'," +
                        "BillingAddress='" + _Item.BillingAddress + "',BillingCity='" + _Item.BillingCity + "',BillingState='" + _Item.BillingState + "'," +
                        "BillingZipcode='" + _Item.BillingZipCode + "',IsShippingAddressSame='" + _Item.IsShippingAddressSame + "',"+
                        "ShippingAddress = '" + _Item.ShippingAddress + "',ShippingCity = '" + _Item.ShippingCity + "',ShippingState = '" + _Item.ShippingState + "'," +
                        "ShippingZipcode='" + _Item.ShippingZipCode + "',IsActive='" + _Item.IsActive + "' WHERE UniqueID='" + _Item.UniqueID + "'";

                    SessionData.SQLDataConnection.Execute(_Query);
                }
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
        }
    }
}
