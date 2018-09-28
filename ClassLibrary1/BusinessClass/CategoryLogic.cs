using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class CategoryLogic
    {
        public List<Category> GetAllCategoryItems()
        {
            try
            {
                string _Query = "Select * from Category where IsActive='true' collate nocase Order By CategoryName asc";

                var _result = SessionData.SQLDataConnection.Query<Category>(_Query);

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

        public List<Category> SearchCategoryItems(string SearchText)
        {
            try
            {
                string _Query = "Select * from Category where CategoryName like '" + SearchText + "%'";

                var _result = SessionData.SQLDataConnection.Query<Category>(_Query);

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

        public void InsertCategoryItem(Category _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO Category(CategoryName,IsActive) VALUES('" + _Item.CategoryName + "','"+_Item.IsActive+"')";

                    SessionData.SQLDataConnection.Execute(_Query);
                }
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
        }

        public void UpdateCategoryItem(Category _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "Update Category Set CategoryName='" + _Item.CategoryName + "',IsActive='"+_Item.IsActive+"' WHERE UniqueID='" + _Item.UniqueID + "'";

                    SessionData.SQLDataConnection.Execute(_Query);
                }
            }
            catch (SQLiteException SQLex)
            {
            }
            catch (Exception Ex)
            {
            }
        }
    }
}
