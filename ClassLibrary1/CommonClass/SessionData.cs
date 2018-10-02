using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class SessionData
    {
        public static OfflineDB _OfflineDB;
        public static SQLiteConnection SQLDataConnection;

        public static double CGST = 2;
        public static double SGST = 2;

        public static string Currency = "₹";
    }
}
