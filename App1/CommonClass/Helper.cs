using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SalesApp.Droid
{
    public class Helper
    {
        private ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

        public bool IsNetworkAvailable()
        {
            try
            {
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                if ((activeConnection != null) && activeConnection.IsConnected)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                //Log
                return false;
            }

        }

        public static bool TrailDateValidation(DateTime _RegisterDate)
        {
            _RegisterDate = _RegisterDate.Date;

            if(_RegisterDate<=DateTime.Now.Date)
            {
                return true;
            }

            return false;
        }
    }
}