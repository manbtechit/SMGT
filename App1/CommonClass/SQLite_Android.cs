using SalesApp.CommonClass;
using SalesApp.Droid;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace SalesApp.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetOfflineConnection()
        {
            try
            {
                var sqliteFilename = "OfflineDB.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
                                                                                                                    //  string documentsPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var path = Path.Combine(documentsPath, sqliteFilename);
                var conn = new SQLite.SQLiteConnection(path, false);
                return conn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}