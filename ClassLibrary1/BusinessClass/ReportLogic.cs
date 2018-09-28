using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class ReportLogic
    {
        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            try
            {
                string _Query = "Select OrderDate, Sum(Total) as Total from PurchaseOrder Group By OrderDate Order By OrderDate asc";

                var _result = SessionData.SQLDataConnection.Query<PurchaseOrder>(_Query);

                return _result;
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
            return null;
        }

    }
}
