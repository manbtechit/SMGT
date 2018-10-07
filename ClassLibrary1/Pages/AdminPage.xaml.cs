
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : OrientationContentPage
    {
        public AdminPage()
        {
            InitializeComponent();

            this.Title = "Manage";
            this.ClassId = "Manage";
            this.BackgroundColor = Color.White;            

            var SuppliertapRecognizer = new TapGestureRecognizer();
            SuppliertapRecognizer.Tapped += async (sender, eventergs) =>
            {
               // await Navigation.PushAsync(new NavigationPage(new SupplierPage() { Title = "Supplier" }));
               await  Utilities.PushAsync(Navigation, new SupplierPage(),"Supplier");
            };
            SupplierImage.GestureRecognizers.Add(SuppliertapRecognizer);
            SupplierText.GestureRecognizers.Add(SuppliertapRecognizer);

            var CustomertapRecognizer = new TapGestureRecognizer();
            CustomertapRecognizer.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new CustomerPage(),"Customer");
            };
            CustomerImage.GestureRecognizers.Add(CustomertapRecognizer);
            CustomerText.GestureRecognizers.Add(CustomertapRecognizer);

            var CategorytapRecognizer = new TapGestureRecognizer();
            CategorytapRecognizer.Tapped += async (sender, eventergs) =>
            {
                await Utilities.PushAsync(Navigation, new CategoryPage(),"Category");
            };
            CategoryImage.GestureRecognizers.Add(CategorytapRecognizer);
            CategoryText.GestureRecognizers.Add(CategorytapRecognizer);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                LoginStack.WidthRequest = Utilities.TabletWidth;
            }
        }
    }
}