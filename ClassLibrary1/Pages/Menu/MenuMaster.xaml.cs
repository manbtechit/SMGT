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
    public partial class MenuMaster : MasterDetailPage
    {
        public MenuMaster()
        {
            InitializeComponent();

            this.Master = new MenuMasterMaster(this) { Icon = "mastermenu.png", Title = "Sales App" };

            this.Detail = new NavigationPage(new Dashboard())
            {
                BarBackgroundColor = Utilities.ThemeColor,
                BarTextColor = Utilities.ThemeTextColor,
                Icon = "Nextarrow.png"
            };

            //if (Device.Idiom == TargetIdiom.Tablet)
            //    MasterBehavior = MasterBehavior.Split;
        }

        NavigationPage _PageNav;

        public void Selected(MenuMasterMenuItem item)
        {
            if (item == null)
                return;

            if (item.Title == "Sync")
            {
                Utilities.InsertData();
                return;
            }

            var page = item.TargetType;
            page.Title = item.Title;
            _PageNav = new NavigationPage(page);

            _PageNav.BarTextColor = Utilities.ThemeTextColor;
            _PageNav.BarBackgroundColor = Utilities.ThemeColor;

            Detail = _PageNav;

            //Detail = new NavigationPage(page)
            //{
            //    BarTextColor = Utilities.ThemeTextColor,
            //    BarBackgroundColor = Utilities.ThemeColor,

            //};

            IsPresented = false;
        }
    }
}