using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        RegisterLogic _registerLogic = new RegisterLogic();

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
            var _Users = _registerLogic.GetAllRegisterUser();
            if (_Users != null && _Users.Count != 0)
            {
                var _loginuser = _Users.Where(i => i.Username.ToLower() == UserNameEntry.Text.ToLower()).FirstOrDefault();
                if(_loginuser!=null)
                {
                    if (_loginuser.Password == PasswordEntry.Text)
                    {
                        SessionData.LoginUserName = UserNameEntry.Text;

                        Navigation.PushModalAsync(new MenuMaster());
                    }
                    else
                        DisplayAlert("Error", "Password incorrect", "Ok");
                }
                else
                    DisplayAlert("Error", "User Name incorrect", "Ok");

                
            }
            else
                DisplayAlert("Message", "User not available. Do the Registration.", "Ok");
        }
    }
}