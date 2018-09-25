using SalesApp.CommonClass;
using SalesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMaster : MasterDetailPage
    {
        public MenuMaster()
        {
            InitializeComponent();

            this.Master = new MenuMasterMaster(this) { Icon = "mastermenu.png", Title = "Personnel Organiser" };
            this.Detail = new NavigationPage(new Dashboard())
            {
                BarBackgroundColor = Utilities.ThemeColor,
                BarTextColor = Utilities.ThemeTextColor,
                Icon = "Nextarrow.png"
            };
        }

        public void Selected(MenuMasterMenuItem item)
        {
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page)
            {
                BarTextColor = Utilities.ThemeTextColor,
                BarBackgroundColor = Utilities.ThemeColor,
            };

            IsPresented = false;
        }
    }
}