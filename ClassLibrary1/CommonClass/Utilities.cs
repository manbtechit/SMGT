using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalesApp
{
    public class Utilities
    {
        public static Color ThemeColor = Color.FromHex("#00456E");
        public static string ThemeColorText = "#00456E";
        public static Color ThemeTextColor = Color.FromHex("#FFFFFF");

        // Light Blue : #4AAECF
        // Gray :  #999999


        public static void PushAsync(INavigation Navigation, Page _PageName)
        {
            Navigation.PushAsync(new NavigationPage(_PageName)
            {
                BarTextColor = ThemeTextColor,
                BarBackgroundColor = ThemeColor
            });
        }

        public static void PushModalAsync(INavigation Navigation, Page _PageName)
        {
            Navigation.PushModalAsync(new NavigationPage(_PageName)
            {
                BarTextColor = ThemeTextColor,
                BarBackgroundColor = ThemeColor
            });
        }
    }
}
