using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        RegisterLogic _registerLogic = new RegisterLogic();

        public Registration()
        {
            InitializeComponent();

            LoginStack.BackgroundColor = Utilities.ThemeColor;
            SaveButton.TextColor = Utilities.ThemeColor;
            CancelButton.TextColor = Utilities.ThemeColor;

            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            string _ErrorMessage = validation();
            if (_ErrorMessage == "")
            {
                var _result = _registerLogic.GetAllRegisterUser();
                if (_result != null)
                {
                    var _item = _result.Where<RegisterUser>(i => i.Username.ToLower() == UserNameEntry.Text.ToLower());
                    if (_item.Count() == 0)
                    {
                        RegisterUser _User = new RegisterUser()
                        {
                            Name = NameEntry.Text,
                            Email = EmailEntry.Text,
                            Mobile = MobileEntry.Text,
                            Username = UserNameEntry.Text,
                            Password = PasswordEntry.Text,
                            DeviceID="Manoj"
                        };

                        _registerLogic.InsertUserItem(_User);

                        await DisplayAlert("Message", "Registered Successfully", "Ok");
                        await Navigation.PopAsync();
                    }
                    else
                        await DisplayAlert("Error", "User name already exists", "Ok");
                }

            }
            else
                await DisplayAlert("Error", _ErrorMessage, "Ok");
        }

        string validation()
        {
            string Error = "";

            if (NameEntry.Text.Trim() == "")
                Error = "Name missing\n";
            if (EmailEntry.Text.Trim() == "")
                Error = "Email missing\n";
            if (MobileEntry.Text.Trim() == "")
                Error = "Mobile missing\n";
            if (UserNameEntry.Text.Trim() == "")
                Error = "User Name missing\n";
            if (PasswordEntry.Text.Trim() == "")
                Error = "Password missing\n";
            if (ConfirmPasswordEntry.Text.Trim() == "")
                Error = "Confirm Password missing\n";
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                Error = "Password mismatch";

            return Error;
        }
    }
}