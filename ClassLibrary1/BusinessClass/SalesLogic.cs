using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SalesApp
{
   public class SalesLogic
    {
        public List<SalesOrder> GetAllSalesOrder()
        {
            try
            {
                string _Query = "Select * from SalesOrder";

                var _result = SessionData.SQLDataConnection.Query<SalesOrder>(_Query);

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

        public List<SalesOrder> GetTodaySalesOrder()
        {
            try
            {
                string _Query = "select * from SalesOrder where " +
                    "substr(OrderDate, 7) || substr(OrderDate, 4, 2) || substr(OrderDate, 1, 2) = '" + DateTime.Now.ToString("yyyyMMdd") + "'";

                var _result = SessionData.SQLDataConnection.Query<SalesOrder>(_Query);

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

        public List<SalesOrder_Product> GetAllSalesProduct(string OrderNumber)
        {
            try
            {
                string _Query = "Select * from SalesOrder_Product WHERE OrderNumber='" + OrderNumber + "'";

                var _result = SessionData.SQLDataConnection.Query<SalesOrder_Product>(_Query);

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

        public List<SalesOrder> GetSearchItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from SalesOrder PO Inner Join SalesOrder_Product POP on PO.OrderNumber =POP.OrderNumber " +
                    "where status='" + PurchaseOrderStatus.Open.ToString() + "' and " +
                    "OrderNumber = '" + SearchText + "' or Supplier like '" + SearchText + "%' or POP.ProductName like '" + SearchText + "%'";

                var _result = SessionData.SQLDataConnection.Query<SalesOrder>(_Query);

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

        public void InsertSalesOrder(SalesOrder _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO SalesOrder(OrderNumber,OrderDate,SubTotal,CGST,SGST,Total," +
                        "ProductCount,Status) VALUES('" +
                        _Item.OrderNumber + "','" + _Item.OrderDate + "','" + _Item.SubTotal + "','" +
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

        public void InsertSalesOrderItems(ObservableCollection<SalesOrder_Product> _ListItem)
        {
            try
            {
                if (_ListItem != null)
                {
                    foreach (var _Item in _ListItem)
                    {
                        string _Query = "INSERT INTO SalesOrder_Product(OrderNumber,ProductID,Quantity,ProductName,SalesPrice, Barcode) VALUES('" +
                            _Item.OrderNumber + "','" + _Item.ProductID + "','" + _Item.Quantity + "','" + _Item.ProductName + "','" + 
                            _Item.SalesPrice + "','" + _Item.Barcode + "')";

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

        public void DeleteSalesOrderItems(string OrderNumber)
        {
            try
            {
                if (OrderNumber != "")
                {
                    string _Query = "Delete from SalesOrder_Product Where OrderNumber='" + OrderNumber + "'";

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

        public void UpdateSalesOrder(SalesOrder _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update SalesOrder Set OrderNumber='" + _Item.OrderNumber + "',OrderDate='" + _Item.OrderDate + "'," +
                        "SubTotal='" + _Item.SubTotal + "'," +
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

        public void UpdateStocks(int ProductID, int Quantity)
        {
            try
            {
                if (ProductID != 0)
                {
                    string _Query = "Select * from Stocks where UniqueID='" + ProductID + "'";
                    var _result = SessionData.SQLDataConnection.Query<Stocks>(_Query).FirstOrDefault();
                    if (_result != null)
                    {
                        Quantity = _result.Quantity - Quantity;

                        _Query = "Update Stocks Set Quantity='" + Quantity + "' Where UniqueID='" + ProductID + "'";
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
    }
}
