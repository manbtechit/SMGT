using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class ReportLogic
    {
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            try
            {
                string _Query = "Select OrderDate, Sum(Total) as Total from PurchaseOrder where " +
                   "substr(OrderDate, 7) || substr(OrderDate, 4, 2) || substr(OrderDate, 1, 2) >= '" + DateTime.Now.AddDays(-10).ToString("yyyyMMdd") + "' " +
                   "and substr(OrderDate,7)|| substr(OrderDate, 4, 2) || substr(OrderDate, 1, 2) <= '" + DateTime.Now.ToString("yyyyMMdd") + "' " +
                   "Group By OrderDate Order By OrderDate desc";

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

        public List<SalesOrder> GetAllSalesOrder()
        {
            try
            {
                string _Query = "Select OrderDate, Sum(Total) as Total from SalesOrder where "+
                    "substr(OrderDate, 7) || substr(OrderDate, 4, 2) || substr(OrderDate, 1, 2) >= '"+DateTime.Now.AddDays(-10).ToString("yyyyMMdd")+"' "+
                    "and substr(OrderDate,7)|| substr(OrderDate, 4, 2) || substr(OrderDate, 1, 2) <= '" + DateTime.Now.ToString("yyyyMMdd") + "' "+
                    "Group By OrderDate Order By OrderDate desc";

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

        public List<ReceiptOrder_Product> GetTodayReceiptTransaction()
        {
            try
            {
                string _Query = "Select ROP.OrderNumber,ROP.ProductName,ROP.Quantity from ReceiptOrder RO INNER JOIN ReceiptOrder_Product ROP "+
                    "ON RO.OrderNumber=ROP.OrderNumber WHERE " +
                    "RO.OrderDate ='" + DateTime.Now.ToString("dd/MM/yyyy") + "' " +
                    "Order By ROP.OrderNumber asc";

                var _result = SessionData.SQLDataConnection.Query<ReceiptOrder_Product>(_Query);

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

        public List<SalesOrder_Product> GetTodaySalesTransaction()
        {
            try
            {
                string _Query = "Select SOP.OrderNumber,SOP.ProductName,SOP.Quantity from SalesOrder SO INNER JOIN SalesOrder_Product SOP " +
                    "ON SO.OrderNumber=SOP.OrderNumber WHERE " +
                    "SO.OrderDate ='" + DateTime.Now.ToString("dd/MM/yyyy") + "' " +
                    "Order By SOP.OrderNumber asc";

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

        public List<Stocks> GetAllStockItems()
        {
            try
            {
                string _Query = "Select * from Stocks ORDER BY ProductName ASC";

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
    }
}
