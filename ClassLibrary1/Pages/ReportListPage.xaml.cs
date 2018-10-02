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

            var TodayTransactionReport = new TapGestureRecognizer();
            TodayTransactionReport.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new ReportDataPage("TodayTransaction"));
            };
            Report1Image.GestureRecognizers.Add(TodayTransactionReport);
            Report1Text.GestureRecognizers.Add(TodayTransactionReport);

            var CurrentStockReport = new TapGestureRecognizer();
            CurrentStockReport.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new ReportDataPage("CurrentStock"));
            };
            Report2Image.GestureRecognizers.Add(CurrentStockReport);
            Report2Text.GestureRecognizers.Add(CurrentStockReport);

            var PurchaseReport = new TapGestureRecognizer();
            PurchaseReport.Tapped += (sender, eventergs) =>
            {

                Utilities.PushModalAsync(Navigation, new ReportDataPage("Purchase"));
            };
            Report3Image.GestureRecognizers.Add(PurchaseReport);
            Report3Text.GestureRecognizers.Add(PurchaseReport);

            var SalesReport = new TapGestureRecognizer();
            SalesReport.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new ReportDataPage("Sales"));
            };
            Report4Image.GestureRecognizers.Add(SalesReport);
            Report4Text.GestureRecognizers.Add(SalesReport);
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Application.Current.MainPage = new MenuMaster();
            return true;
        }
    }
}