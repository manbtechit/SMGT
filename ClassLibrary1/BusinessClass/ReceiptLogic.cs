using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SalesApp
{
    public class ReceiptLogic
    {
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            try
            {
                string _Query = "Select * from PurchaseOrder where status='" + PurchaseOrderStatus.Open.ToString() + "'";

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

        public List<ReceiptOrder> GetAllReceiptOrder()
        {
            try
            {
                string _Query = "Select * from ReceiptOrder";

                var _result = SessionData.SQLDataConnection.Query<ReceiptOrder>(_Query);

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
                string _Query = "Select * from PurchaseOrder_Product WHERE OrderNumber='" + OrderNumber + "'";

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
                string _Query = "Select * from PurchaseOrder where ProductName = '" + SearchText + "'";

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

        public List<PurchaseOrder> GetSearchItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from PurchaseOrder PO Inner Join PurchaseOrder_Product POP on PO.OrderNumber =POP.OrderNumber " +
                    "where status='" + PurchaseOrderStatus.Open.ToString() + "' and " +
                    "OrderNumber = '" + SearchText + "' or Supplier like '" + SearchText + "%' or POP.ProductName like '" + SearchText + "%'";

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

        public void InsertReceiptOrder(ReceiptOrder _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO ReceiptOrder(OrderNumber,OrderDate,ReceiptDate,Supplier,SubTotal,CGST,SGST,Total," +
                        "ProductCount,Status) VALUES('" +
                        _Item.OrderNumber + "','" + _Item.OrderDate + "','"+_Item.ReceiptDate+"','" + 
                        _Item.Supplier + "','" + _Item.SubTotal + "','" +
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

        public void InsertReceiptOrderItems(ObservableCollection<PurchaseOrder_Product> _ListItem)
        {
            try
            {
                if (_ListItem != null)
                {
                    foreach (var _Item in _ListItem)
                    {
                        string _Query = "INSERT INTO ReceiptOrder_Product(OrderNumber,ProductID,Quantity,ProductName,PurchasePrice,Barcode) VALUES('" +
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

        public void UpdatePurchaseOrder(string OrderNumber)
        {
            try
            {
                if (OrderNumber != "")
                {
                    string _Query = "Update PurchaseOrder Set Status='"+PurchaseOrderStatus.Closed+"' Where OrderNumber='" + OrderNumber + "'";

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

        public void UpdateStocks(int ProductID,int Quantity)
        {
            try
            {
                if (ProductID != 0)
                {
                    string _Query = "Select * from Stocks where UniqueID='" + ProductID+"'";
                    var _result = SessionData.SQLDataConnection.Query<Stocks>(_Query).FirstOrDefault();
                    if (_result != null)
                    {
                        Quantity = _result.Quantity + Quantity;

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
