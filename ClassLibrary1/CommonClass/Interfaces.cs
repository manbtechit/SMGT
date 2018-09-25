using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp.CommonClass
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetOfflineConnection();
    }
}
