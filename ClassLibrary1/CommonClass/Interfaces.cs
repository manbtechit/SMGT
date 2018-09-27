using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetOfflineConnection();
    }
}
