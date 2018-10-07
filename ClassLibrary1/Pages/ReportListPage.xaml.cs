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
    public partial class ReportListPage : ContentPage
    {
        public ReportListPage()
        {
            InitializeComponent();

            this.Title = "Reports";
            this.ClassId = "Reports";
            this.BackgroundColor = Color.White;
            var TodayTransactionReport = new TapGestureRecognizer();
            TodayTransactionReport.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new ReportDataPage("TodayTransaction"), "Today Transaction");
            };
            Report1Image.GestureRecognizers.Add(TodayTransactionReport);
            Report1Text.GestureRecognizers.Add(TodayTransactionReport);

            var CurrentStockReport = new TapGestureRecognizer();
            CurrentStockReport.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new ReportDataPage("CurrentStock"), "Current Stock");
            };
            Report2Image.GestureRecognizers.Add(CurrentStockReport);
            Report2Text.GestureRecognizers.Add(CurrentStockReport);

            var PurchaseReport = new TapGestureRecognizer();
            PurchaseReport.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new ReportDataPage("Purchase"), "Purchase Order Report");
            };
            Report3Image.GestureRecognizers.Add(PurchaseReport);
            Report3Text.GestureRecognizers.Add(PurchaseReport);

            var SalesReport = new TapGestureRecognizer();
            SalesReport.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new ReportDataPage("Sales"), "Sales Order Report");
            };
            Report4Image.GestureRecognizers.Add(SalesReport);
            Report4Text.GestureRecognizers.Add(SalesReport);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                LoginStack.WidthRequest = Utilities.TabletWidth;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Application.Current.MainPage = new MenuMaster();
            return true;
        }
    }
}