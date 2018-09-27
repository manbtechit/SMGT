using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string CategoryName { get; set; }
        public string IsActive { get; set; }
    }

    public class Supplier
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
        public string Notes { get; set; }
        public string IsActive { get; set; }
    }

    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int UniqueID { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
        public string IsShippingAddressSame { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string IsActive { get; set; }
    }
}
