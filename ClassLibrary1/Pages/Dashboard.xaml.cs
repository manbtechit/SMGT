using SalesApp.CommonClass;
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
	public partial class Dashboard : ContentPage
	{
		public Dashboard ()
		{
			InitializeComponent ();

            this.Title = "Dashboard";

            LoadClicks();
        }

        void LoadClicks()
        {
            var StocktapRecognizer = new TapGestureRecognizer();
            StocktapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new StockOverview());
            };
            ImageStock.GestureRecognizers.Add(StocktapRecognizer);
            LabelStock.GestureRecognizers.Add(StocktapRecognizer);

            var PurchasetapRecognizer = new TapGestureRecognizer();
            PurchasetapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new PurchaseOrderList());
            };
            ImagePurchase.GestureRecognizers.Add(PurchasetapRecognizer);
            LabelPurhase.GestureRecognizers.Add(PurchasetapRecognizer);

            var ReceipttapRecognizer = new TapGestureRecognizer();
            ReceipttapRecognizer.Tapped += (sender, eventergs) => {
                //Utilities.PushModalAsync(Navigation, new rec());
            };
            ImageReceipt.GestureRecognizers.Add(ReceipttapRecognizer);
            LabelReceipt.GestureRecognizers.Add(ReceipttapRecognizer);

            var SaletapRecognizer = new TapGestureRecognizer();
            SaletapRecognizer.Tapped += (sender, eventergs) => {
                // Utilities.PushModalAsync(Navigation, new SupplierManage());
            };
            ImageSale.GestureRecognizers.Add(SaletapRecognizer);
            LabelSale.GestureRecognizers.Add(SaletapRecognizer);
        }
    }
}