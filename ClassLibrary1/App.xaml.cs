using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SalesApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            SessionData._OfflineDB = new OfflineDB();
            SessionData.SQLDataConnection = DependencyService.Get<ISQLite>().GetOfflineConnection();

            SessionData.LoginUserName = "Manoj Kumar";

            //Below line used to insert Master data to the app
          //  Utilities.InsertData();

            MainPage = new MenuMaster();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
