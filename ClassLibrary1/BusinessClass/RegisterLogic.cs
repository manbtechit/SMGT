using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApp
{
    public class RegisterLogic
    {

        public List<RegisterUser> GetAllRegisterUser()
        {
            try
            {
                string _Query = "Select * from RegisterUser";

                var _result = SessionData.SQLDataConnection.Query<RegisterUser>(_Query);

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


        public void InsertUserItem(RegisterUser _Item)
        {
            try
            {
                if (_Item != null)
                {
                    string _Query = "INSERT INTO RegisterUser(Name,Email,Mobile,UserName,Password,CreatedBy,CreatedDate,DeviceID) VALUES('" +
                        _Item.Name + "','" + _Item.Email + "','" + _Item.Mobile + "','" + _Item.Username + "','" +
                        _Item.Password + "','"+_Item.Username+"','"+DateTime.Now.ToString("dd/MM/yyyy")+"','"+_Item.DeviceID+"')";

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
