﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class Stocks
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double SalesPrice { get; set; }
        public string Unit { get; set; }
        public string Supplier { get; set; }
        public int AlertQuantity { get; set; }
        public string Barcode { get; set; }
        public string ProductDescription { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class PurchaseOrder
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string Supplier { get; set; }
        public double SubTotal { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double Total { get; set; }
        public double ProductCount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class PurchaseOrder_Product
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public string Barcode { get; set; }
    }

    public class ReceiptOrder
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string ReceiptDate { get; set; }
        public string Supplier { get; set; }
        public double SubTotal { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double Total { get; set; }
        public double ProductCount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class ReceiptOrder_Product
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public string Barcode { get; set; }
    }

    public class SalesOrder
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string Customer { get; set; }
        public double SubTotal { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double Disount { get; set; }
        public double Total { get; set; }
        public double ProductCount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class SalesOrder_Product
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string OrderNumber { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double SalesPrice { get; set; }
        public string Barcode { get; set; }
    }

    public class StockReport
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public string Category { get; set; }
        public string Quantity { get; set; }
        public string PurchasePrice { get; set; }
        public string SalesPrice { get; set; }
        public string Unit { get; set; }
        public string Supplier { get; set; }
        public int AlertQuantity { get; set; }
        public string Barcode { get; set; }
        public string ProductDescription { get; set; }
        public string IsActive { get; set; }
      
    }
}
