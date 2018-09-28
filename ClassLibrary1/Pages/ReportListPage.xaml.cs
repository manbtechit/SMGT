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
		public ReportListPage ()
		{
			InitializeComponent ();

            var PurchaseReport = new TapGestureRecognizer();
            PurchaseReport.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new ReportDataPage("Purchase"));
            };
            Report3Image.GestureRecognizers.Add(PurchaseReport);
            Report3Text.GestureRecognizers.Add(PurchaseReport);
        }
    }
}