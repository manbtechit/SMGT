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
    public partial class AdminManage : ContentPage
    {
        public AdminManage()
        {
            InitializeComponent();

            var SuppliertapRecognizer = new TapGestureRecognizer();
            SuppliertapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new SupplierManage());
            };
            SupplierImage.GestureRecognizers.Add(SuppliertapRecognizer);
            SupplierText.GestureRecognizers.Add(SuppliertapRecognizer);

            var CustomertapRecognizer = new TapGestureRecognizer();
            CustomertapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new CustomerManage());
            };
            CustomerImage.GestureRecognizers.Add(CustomertapRecognizer);
            CustomerText.GestureRecognizers.Add(CustomertapRecognizer);

            var CategorytapRecognizer = new TapGestureRecognizer();
            CategorytapRecognizer.Tapped += (sender, eventergs) => {
                Utilities.PushModalAsync(Navigation, new CategoryManage());
            };
            CategoryImage.GestureRecognizers.Add(CategorytapRecognizer);
            CategoryText.GestureRecognizers.Add(CategorytapRecognizer);

        }
    }
}