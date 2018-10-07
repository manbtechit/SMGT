using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SalesApp
{
    public class Utilities
    {
        public static Color ThemeColor = Color.FromHex("#00456E");
        public static string ThemeColorText = "#00456E";
        public static Color ThemeTextColor = Color.FromHex("#FFFFFF");

        public static int TabletWidth = 500;

        // Light Blue : #4AAECF
        // Gray :  #999999

        public static List<string> Units = new List<string>() {
            "Select Unit","Bag","Bags","Boxes","Gm","Jar","Kg","Ltr","Ml","Numbers","Packet","Pcs","Unit"
        };

        public async static Task PushAsync(INavigation Navigation, Page _PageName,string Pagename)
        {
          await  Navigation.PushAsync(new NavigationPage(_PageName)
            {
                BarTextColor = ThemeTextColor,
                BarBackgroundColor = ThemeColor,
                ClassId = Pagename
          });
        }

        public async static void PushModalAsync(INavigation Navigation, Page _PageName)
        {
           await Navigation.PushModalAsync(new NavigationPage(_PageName)
            {
                BarTextColor = ThemeTextColor,
                BarBackgroundColor = ThemeColor
            });
        }

        public static void InsertData()
        {
            SessionData.SQLDataConnection.Execute("Delete from Category");
            SessionData.SQLDataConnection.Execute("Delete from Supplier");
            SessionData.SQLDataConnection.Execute("Delete from Customer");
            SessionData.SQLDataConnection.Execute("Delete from Stocks");            

            //Category Table values
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Oil','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Rice','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Chocolate','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Drinks','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Masala','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Stationary','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Flour','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Dals','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Pickels','True')");
            SessionData.SQLDataConnection.Execute("Insert into Category(CategoryName,IsActive) values('Dry Fruits','True')");

            //Supplier Table Values
            SessionData.SQLDataConnection.Execute("Insert into Supplier(SupplierName,CompanyName,Phone,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Manoj Kumar', 'Manoj', '12345', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Supplier(SupplierName,CompanyName,Phone,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Venkat', 'Venkat', '12345', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'US', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Supplier(SupplierName,CompanyName,Phone,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Santhosh Kumar', 'Santhosh', '12345', '12345', 'manbtechit@gmail.com', 'Thiruvanamalai', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Supplier(SupplierName,CompanyName,Phone,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Mahendra Babu', 'Mahendra', '12345', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Supplier(SupplierName,CompanyName,Phone,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Uma Shankar', 'Uma', '12345', '12345', 'manbtechit@gmail.com', 'UK', 'Chennai', 'Tamilnadu', '600100', 'True')");

            //Customer Table Values
            SessionData.SQLDataConnection.Execute("Insert into Customer(CustomerName,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Nithya', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Customer(CustomerName,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Vanitha', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'US', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Customer(CustomerName,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Manju', '12345', 'manbtechit@gmail.com', 'Thiruvanamalai', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Customer(CustomerName,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Narmatha', '12345', 'manbtechit@gmail.com', 'Medavakkam', 'Chennai', 'Tamilnadu', '600100', 'True')");
            SessionData.SQLDataConnection.Execute("Insert into Customer(CustomerName,Mobile,Email," +
                "BillingAddress, BillingCity, BillingState, BillingZipCode, IsActive) Values('Savitha', '12345', 'manbtechit@gmail.com', 'UK', 'Chennai', 'Tamilnadu', '600100', 'True')");


            //Stock Items
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Pepsi','122','Drinks','100','24','25','Ltr','Manoj Kumar','10','12345','Pepsi Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Coca-cola','12222','Drinks','100','24','25','Ltr','Manoj Kumar','10','12345','Coca-cola Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Maaza','12232','Drinks','100','14','15','Ltr','Manoj Kumar','10','1442345','Maaza Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('India Gate Basmathi','12552','Rice','100','50','55','Kg','Venkat','10','1234555','India Gate Basmathi Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Double Deer Basmathi','12432','Rice','100','50','51','kg','Venkat','10','1234675','Double Deer Basmathi Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Dairy Milk','18922','Chocolate','100','24','25','Pcs','Santhosh Kumar','10','12344445','Dairy Milk Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Bounty Mars','132222','Chocolate','100','30','32','Pcs','Santhosh Kumar','10','1234555','Bounty Mars Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Gold Winner','11122','Oil','100','24','25','Ltr','Mahendra Babu','10','12345088','Gold Winner Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Fortune','11222','Oil','100','24','25','Ltr','Mahendra Babu','10','12345099','Fortune Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Sunland','123322','Oil','100','24','25','Ltr','Mahendra Babu','10','12345890','Sunland Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Aachi Chicken Masala','127722','Masala','100','24','25','Kg','Uma Shankar','10','1234583390','Aachi Chicken Masala Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('MTR Fish Fry Masala','1238822','Masala','100','24','25','Kg','Uma Shankar','10','1234544890','Sunland Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('777 Madras Mango','129022','Pickels','100','24','25','kg','Mahendra Babu','10','123432578','777 Madras Mango Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('777 Tomato Pickle','123322','Pickels','100','24','25','kg','Mahendra Babu','10','12345888','777 Tomato Pickle Product','True','Manoj','03/10/2018')");
            SessionData.SQLDataConnection.Execute("Insert into Stocks(ProductName,ProductNumber,Category,Quantity,PurchasePrice,SalesPrice,Unit,Supplier,AlertQuantity,Barcode,ProductDescription,IsActive,CreatedBy,CreatedDate) Values('Aachi Lemon Pickle','123322','Pickels','100','24','25','kg','Mahendra Babu','10','123458657','Aachi Lemon Pickle Product','True','Manoj','03/10/2018')");

        }
    }
}
