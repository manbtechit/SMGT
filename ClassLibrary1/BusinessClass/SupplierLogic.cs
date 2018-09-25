using SalesApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp.BusinessClass
{
   public class SupplierLogic
    {
        public List<Supplier> GetAllSupplierItems()
        {
            try
            {
                string _Query = "Select * from Supplier where IsActive='true' collate nocase";

                var _result = SessionData.SQLDataConnection.Query<Supplier>(_Query);

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

        public List<Supplier> SearchSupplierItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from Supplier where SupplierName like '" + SearchText + "%'";

                var _result = SessionData.SQLDataConnection.Query<Supplier>(_Query);

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

        public void InsertSupplierItem(Supplier _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO Supplier(SupplierName,CompanyName,Phone,Mobile,Email,BillingAddress,BillingCity,"+
                        "BillingState,BillingZipcode,Notes,IsActive) VALUES('" +
                        _Item.SupplierName + "','" + _Item.CompanyName + "','" + _Item.Phone + "','" + _Item.Mobile + "','" +
                        _Item.Email + "','" + _Item.BillingAddress + "','" + _Item.BillingCity + "','" + _Item.BillingState + "','" +
                        _Item.BillingZipCode + "','" + _Item.Notes + "','" + _Item.IsActive + "')";

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

        public void UpdateSupplierItem(Supplier _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update Supplier Set SupplierName='" + _Item.SupplierName + "',CompanyName='" + _Item.CompanyName + "'," +
                        "Phone='" + _Item.Phone + "',Mobile='" + _Item.Mobile + "',Email='" + _Item.Email + "'," +
                        "BillingAddress='" + _Item.BillingAddress + "',BillingCity='" + _Item.BillingCity + "',BillingState='" + _Item.BillingState + "'," +
                        "BillingZipcode='" + _Item.BillingZipCode + "',Notes='" + _Item.Notes + "',IsActive='" + _Item.IsActive + "' WHERE UniqueID='" + _Item.UniqueID + "'";

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
