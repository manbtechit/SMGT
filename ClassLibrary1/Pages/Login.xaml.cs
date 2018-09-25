using SalesApp.CommonClass;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();

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