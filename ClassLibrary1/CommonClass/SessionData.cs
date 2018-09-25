using SalesApp.CommonClass;
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
    }
}
