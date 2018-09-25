using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp.Model
{
    public class RootObject
    {
        public string QueryResults { get; set; }
        public string InsertPrefix { get; set; }
        public string Inserts { get; set; }
        public bool QuerySuccessful { get; set; }
        public int ElapsedTime { get; set; }
        public int Rows { get; set; }
    }

    public class Stocks
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public int PurchasePrice { get; set; }
        public int SalesPrice { get; set; }
        public string Unit { get; set; }
        public string Supplier { get; set; }
        public int AlertQuantity { get; set; }
        public string Barcode { get; set; }
        public string ProductDescription { get; set; }
        public string IsActive { get; set; }
    }

    public class PurchaseOrder
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string Supplier { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double Total { get; set; }
        public double ProductCount { get; set; }
        public string Status { get; set; }
    }

    public class PurchaseOrder_Product
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
