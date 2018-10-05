using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetOfflineConnection();

        void CreateUser(RegisterUser Users);

        Task<List<RegisterUser>> LoadData();
    }
}
