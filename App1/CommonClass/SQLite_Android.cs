using Android.App;
using SalesApp;
using SalesApp.Droid;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
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

        public static async Task CopyDatabaseAsync(Activity activity)
        {
            var SourcedocumentsPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "OfflineDB.db3");
            var DestinationdocumentsPath = System.IO.Path.Combine("/storage/emulated/0/DB/", "OfflineDB.db3");

            if (System.IO.File.Exists(SourcedocumentsPath))
            {
                try
                {
                    if (System.IO.File.Exists(SourcedocumentsPath))
                    {
                        var dir = new Java.IO.File("/storage/emulated/0/DB/");
                        if (!dir.Exists())
                            dir.Mkdirs();

                        System.IO.File.Copy(SourcedocumentsPath, DestinationdocumentsPath, true);
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
        }
    }
}