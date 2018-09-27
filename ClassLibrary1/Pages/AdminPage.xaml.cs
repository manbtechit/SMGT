
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminPage : ContentPage
	{
		public AdminPage ()
		{
			InitializeComponent ();

            var SuppliertapRecognizer = new TapGestureRecognizer();
            SuppliertapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new SupplierPage());
            };
            SupplierImage.GestureRecognizers.Add(SuppliertapRecognizer);
            SupplierText.GestureRecognizers.Add(SuppliertapRecognizer);

            var CustomertapRecognizer = new TapGestureRecognizer();
            CustomertapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new CustomerPage());
            };
            CustomerImage.GestureRecognizers.Add(CustomertapRecognizer);
            CustomerText.GestureRecognizers.Add(CustomertapRecognizer);

            var CategorytapRecognizer = new TapGestureRecognizer();
            CategorytapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new CategoryPage());
            };
            CategoryImage.GestureRecognizers.Add(CategorytapRecognizer);
            CategoryText.GestureRecognizers.Add(CategorytapRecognizer);
        }
	}
}