﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);

            LoginStack.BackgroundColor = Utilities.ThemeColor;
            LoginButton.TextColor = Utilities.ThemeColor;
            LoginButton.Clicked += LoginButton_Clicked;

        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
          Navigation.PushModalAsync(new MenuMaster());
        }
    }
}