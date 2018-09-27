using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SalesApp
{
   public class OfflineDB
    {
        private SQLiteConnection offlineDataBase;

        //Creation of all Offline DB tables based on Model
        public OfflineDB()
        {
            try
            {
                offlineDataBase = DependencyService.Get<ISQLite>().GetOfflineConnection();

                checkAndCreateTable<Stocks>();
                checkAndCreateTable<Category>();
                checkAndCreateTable<Supplier>();
                checkAndCreateTable<Customer>();

                checkAndCreateTable<PurchaseOrder>();
                checkAndCreateTable<PurchaseOrder_Product>();

                checkAndCreateTable<SalesOrder>();
                checkAndCreateTable<SalesOrder_Product>();
            }
            catch (Exception ex)
            {

            }
        }

        //Check and create table if not available
        private void checkAndCreateTable<T>()
        {
            try
            {
                var info = offlineDataBase.GetTableInfo(typeof(T).Name);
                if (info != null)
                    offlineDataBase.CreateTable<T>(CreateFlags.None);
            }
            catch (Exception ex)
            {

            }
        }

        //Add item to a table
        public int AddItem<T>(T item)
        {
            lock (offlineDataBase)
            {
                return offlineDataBase.Insert(item);
            }
        }

        //Update item to the table
        public int UpdateItem<T>(T item)
        {
            try
            {
                lock (offlineDataBase)
                {
                    return offlineDataBase.Update(item);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int InsertAll<T>(List<T> item)
        {
            try
            {
                lock (offlineDataBase)
                {
                    return offlineDataBase.InsertAll(item);
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteAll<T>()
        {
            try
            {
                lock (offlineDataBase)
                {
                    return offlineDataBase.DeleteAll<T>();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteItem<T>(T item)
        {
            lock (offlineDataBase)
            {
                return offlineDataBase.Delete(item);
            }
        }

        public int InsertOrUpdate<T>(T p_item)
        {
            try
            {
                return offlineDataBase.InsertOrReplace(p_item, typeof(T));

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<T> GetData<T>(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition = null) where T : new()
        {
            lock (offlineDataBase)
            {
                try
                {
                    if (whereCondition != null)
                        return offlineDataBase.Table<T>().Where(whereCondition).ToList();
                    else
                    {
                        var t = offlineDataBase.Table<T>().ToList();
                        return t;
                    }
                }
                catch
                {
                    return new List<T>();
                }
            }
        }
    }
}
