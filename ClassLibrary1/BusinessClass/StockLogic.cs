using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class StockLogic
    {
        public List<Stocks> GetAllStockItems()
        {
            try
            {
                string _Query = "Select * from Stocks where IsActive='true' COLLATE NOCASE";

                var _result = SessionData.SQLDataConnection.Query<Stocks>(_Query);

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

        public List<Stocks> SearchStockItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from Stocks where ProductName like '"+SearchText+"%'";

                var _result = SessionData.SQLDataConnection.Query<Stocks>(_Query);

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

        public void InsertStockItem(Stocks _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,"+
                        "AlertQuantity,Barcode,ProductDescription,IsActive) VALUES('" +
                        _Item.ProductName + "','" + _Item.ProductNumber + "','" + _Item.Category + "','" + _Item.Quantity + "','" + 
                        _Item.PurchasePrice + "','" + _Item.SalesPrice + "','" + _Item.Unit + "','" + _Item.Supplier + "','" + 
                        _Item.AlertQuantity + "','" + _Item.Barcode + "','" + _Item.ProductDescription + "','" + _Item.IsActive + "')";

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

        public void UpdateStockItem(Stocks _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update Stocks Set ProductName='" + _Item.ProductName + "',ProductNumber='" + _Item.ProductNumber + "'," +
                        "Quantity='" + _Item.Quantity + "',Category='"+_Item.Category+ "',PurchasePrice='" + _Item.PurchasePrice + "',"+
                        "SalesPrice='" + _Item.SalesPrice + "',Unit='" + _Item.Unit + "',Supplier='" + _Item.Supplier + "',"+
                        "AlertQuantity='" + _Item.AlertQuantity + "',Barcode='" + _Item.Barcode + "',"+
                        "ProductDescription='" + _Item.ProductDescription + "',IsActive='"+ _Item.IsActive + "' WHERE UniqueID='" + _Item.UniqueID + "'";

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
