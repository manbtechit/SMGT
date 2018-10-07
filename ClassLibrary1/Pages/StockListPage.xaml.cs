using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockListPage : ContentPage
    {
        StockLogic _stockLogic = new StockLogic();
        CategoryLogic _categoryLogic = new CategoryLogic();
        Stocks _EditItem;
        SupplierLogic _supplierLogic = new SupplierLogic();
        ZXingScannerPage scanPage;

        private static ObservableCollection<Stocks> _stockcollection = null;

        public static ObservableCollection<Stocks> StockAssestList
        {
            get
            {
                if (_stockcollection == null)
                {
                    _stockcollection = new ObservableCollection<Stocks>();
                }
                return _stockcollection;
            }
            set { _stockcollection = value; }
        }

        public StockListPage()
        {
            InitializeComponent();

            this.Title = "";
            this.ClassId = "Stocks";
            this.BackgroundColor = Color.White;
            ButtonEdit.Clicked += ButtonEdit_Clicked;

            ListStock.IsPullToRefreshEnabled = false;
            ListStock.IsRefreshing = IsBusy;
            ChangeLayout(true);

            ListStock.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (Stocks)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            ToolbarItems.Add(new ToolbarItem("Add", "Add.png", async () =>
            {
                _EditItem = null;
                await Utilities.PushAsync(Navigation, new StockDetailsPage(null), "Stock Details");
            }));

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            LoadList();

            BarcodeInit();

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                OverallStack.WidthRequest = Utilities.TabletWidth;
            }
        }

        void AssignValues(Stocks _Item)
        {
            LblProductName.Text = _Item.ProductName + "  (No. " + _Item.ProductNumber + ")";
            LblBarcode.Text = "Barcode : " + _Item.Barcode;

            LblCategory.Text = "Category : " + _Item.Category;
            LblQuantity.Text = "Quantity : " + _Item.Quantity.ToString() + " " + _Item.Unit;

            LblAlertQuantity.Text = "Alert Quantity : " + _Item.AlertQuantity.ToString() + " " + _Item.Unit;

            LblPurchasePrice.Text = "Purchase Price : " + _Item.PurchasePrice.ToString();
            LblSalesPrice.Text = "Sale Price : " + _Item.SalesPrice.ToString();

            LblSupplier.Text = "Supplier : " + _Item.Supplier;

            LblActive.Text = "Active status : " + _Item.IsActive;

            if (_Item.ProductDescription.Trim() != "")
                LblProductDescription.Text = _Item.ProductDescription;
            else
                LblProductDescription.Text = "Not available";
        }

        void ChangeLayout(bool _Value)
        {
            ListContent.IsVisible = _Value;
            FormContent.IsVisible = !_Value;
        }

        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            await Utilities.PushAsync(Navigation, new StockDetailsPage(_EditItem), "Stock Details");
        }

        void LoadList()
        {
            var _stockItems = _stockLogic.GetAllStockItems();
            if (_stockItems != null && _stockItems.Count != 0)
            {
                StockAssestList.Clear();

                foreach (var _Items in _stockItems)
                    StockAssestList.Add(_Items);

                ListStock.ItemsSource = StockAssestList;
            }
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _stockLogic.SearchStockItems(EntrySearch.Text);
            if (_Filter != null)
            {
                StockAssestList.Clear();

                foreach (var _Items in _Filter)
                    StockAssestList.Add(_Items);

                ListStock.ItemsSource = StockAssestList;
            }
            else
            {
                ListStock.ItemsSource = null;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            if (FormContent.IsVisible)
            {
                ChangeLayout(true);
                return true;
            }
            else
                return false;
        }

        void BarcodeInit()
        {
            var BarcodeImageTap = new TapGestureRecognizer();
            BarcodeImageTap.Tapped += async delegate
            {
                // Create our custom overlay
                var customOverlay = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                var torch = new Button
                {
                    Text = "Toggle Torch"
                };
                torch.Clicked += delegate
                {
                    scanPage.ToggleTorch();
                };
                customOverlay.Children.Add(torch);

                scanPage = new ZXingScannerPage(new ZXing.Mobile.MobileBarcodeScanningOptions { AutoRotate = true }, customOverlay: customOverlay);
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync();

                        EntrySearch.Text = result.Text;

                        EntrySearch_TextChanged(null, null);
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            };

            SearchBarcode.GestureRecognizers.Add(BarcodeImageTap);
        }
    }
}