using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SalesApp
{
    public class POLogic
    {
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            try
            {
                string _Query = "Select * from PurchaseOrder";

                var _result = SessionData.SQLDataConnection.Query<PurchaseOrder>(_Query);

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

        public List<PurchaseOrder_Product> GetAllPurchaseProduct(string OrderNumber)
        {
            try
            {
                string _Query = "Select * from PurchaseOrder_Product WHERE OrderNumber='"+OrderNumber+"'";

                var _result = SessionData.SQLDataConnection.Query<PurchaseOrder_Product>(_Query);

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

        public List<Stocks> GetStockItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from Stocks where ProductName = '" + SearchText + "'";

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

        public Stocks GetStockItembyBarcode(string BarcodeText)
        {
            try
            {
                string _Query = "Select * from Stocks where Barcode = '" + BarcodeText + "'";

                var _result = SessionData.SQLDataConnection.Query<Stocks>(_Query).FirstOrDefault();

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

        public void InsertPurchaseOrder(PurchaseOrder _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO PurchaseOrder(OrderNumber,OrderDate,Supplier,SubTotal,CGST,SGST,Total," +
                        "ProductCount,Status) VALUES('" +
                        _Item.OrderNumber + "','" + _Item.OrderDate + "','" + _Item.Supplier + "','" + _Item.SubTotal + "','" +
                        _Item.CGST + "','" + _Item.SGST + "','" + _Item.Total + "','" +
                        _Item.ProductCount + "','" + _Item.Status + "')";

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

        public void InsertPurchaseOrderItems(ObservableCollection<PurchaseOrder_Product> _ListItem)
        {
            try
            {
                if (_ListItem != null)
                {
                    foreach (var _Item in _ListItem)
                    {
                        string _Query = "INSERT INTO PurchaseOrder_Product(OrderNumber,ProductID,Quantity,ProductName,PurchasePrice,Barcode) VALUES('" +
                            _Item.OrderNumber + "','" + _Item.ProductID + "','" + _Item.Quantity + "','" + 
                            _Item.ProductName + "','" + _Item.PurchasePrice + "','" + _Item.Barcode + "')";

                        SessionData.SQLDataConnection.Execute(_Query);
                    }
                }
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
        }

        public void DeletePurchaseOrderItems(string OrderNumber)
        {
            try
            {
                if (OrderNumber != "")
                {
                    string _Query = "Delete from PurchaseOrder_Product Where OrderNumber='" + OrderNumber + "'";

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

        public void UpdateStockItem(PurchaseOrder _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update PurchaseOrder Set OrderNumber='" + _Item.OrderNumber + "',OrderDate='" + _Item.OrderDate + "'," +
                        "Supplier='" + _Item.Supplier + "',SubTotal='" + _Item.SubTotal + "'," +
                        "CGST='" + _Item.CGST + "',SGST='" + _Item.SGST + "',Total='" + _Item.Total + "'," +
                        "ProductCount='" + _Item.ProductCount + "',Status='" + _Item.Status + "' WHERE UniqueID='" + _Item.UniqueID + "'";

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
