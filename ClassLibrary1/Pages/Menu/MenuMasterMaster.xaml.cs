using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMasterMaster : ContentPage
    {
        public ListView ListView;
        MenuMaster _MenuMaster;
        
        public MenuMasterMaster(MenuMaster MenuMast)
        {
            InitializeComponent();

            GridListView.BackgroundColor = Utilities.ThemeColor;

            BindingContext = new MenuMasterMasterViewModel();
            ListView = MenuItemsListView;

            this._MenuMaster = MenuMast;

            ListView.ItemSelected += OnSelectedItem;

            LalbelTitle.Text = "Welcome : Manoj Kumar";

            var LogouttapRecognizer = new TapGestureRecognizer();
            LogouttapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new Login());
            };
            LalbelLogout.GestureRecognizers.Add(LogouttapRecognizer);
        }

        void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {

            var SelectedItem = e.SelectedItem as MenuMasterMenuItem;
            if (SelectedItem != null)
            {
                _MenuMaster.Selected(SelectedItem);
                ListView.SelectedItem = null;
            }
            else
                return;
        }

        class MenuMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MenuMasterMenuItem> MenuItems { get; set; }
            
            public MenuMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<MenuMasterMenuItem>(new[]
                {
                    new MenuMasterMenuItem { Id = 0, Title = "Dashboard", ImageUrl="icon_Dashborad.png", TargetType=typeof(Dashboard) },
                    new MenuMasterMenuItem { Id = 1, Title = "Stock Overview", ImageUrl="icon_Stocks.png", TargetType=typeof(StockListPage) },
                    new MenuMasterMenuItem { Id = 2, Title = "Purchase Order",ImageUrl="icon_Purchase.png", TargetType=typeof(PurchaseOrderPage) },
                    new MenuMasterMenuItem { Id = 3, Title = "Receipt", ImageUrl="icon_Receipt.png",TargetType=typeof(ReceiptPage) },
                    new MenuMasterMenuItem { Id = 4, Title = "Sales Order", ImageUrl="icon_Sales.png" ,TargetType=typeof(SalesPage) },
                    new MenuMasterMenuItem { Id = 5, Title = "Reports", ImageUrl="icon_Reports.png",TargetType=typeof(ReportListPage) },
                    new MenuMasterMenuItem { Id = 6, Title = "Manage", ImageUrl="icon_Manage.png",TargetType=typeof(AdminPage) },
                    new MenuMasterMenuItem { Id = 7, Title = "Sync", ImageUrl="icon_sync.png" }
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}