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
    public partial class StockDetailsPage : ContentPage
    {
        StockLogic _stockLogic = new StockLogic();
        CategoryLogic _categoryLogic = new CategoryLogic();
        Stocks _EditItem;
        SupplierLogic _supplierLogic = new SupplierLogic();
        ZXingScannerPage scanPage;

        public StockDetailsPage(Stocks EditData)
        {
            InitializeComponent();

            this.Title = "Stock Details";
            this.ClassId = "Stock Details";
            this.BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, false);

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;

            EntryProductName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            EntryProductNumber.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            EntryProductDescription.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);

            if (EditData == null)
                ClearValues();
            else
            {
                _EditItem = EditData;
                AssignValues(_EditItem);
            }

            BarcodeInit();

            EntryKeyEvents();

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                OverallStack.WidthRequest = Utilities.TabletWidth;
            }
        }

        void LoadPicker()
        {
            List<string> _UnitItems = Utilities.Units;
            
            PickerUnit.Items.Clear();
            PickerUnit.Title = "Select Unit";
            PickerUnit.ItemsSource = _UnitItems;
            PickerUnit.TextColor = Color.FromHex("#000000");
            PickerUnit.SelectedIndex = 0;

            List<string> _CategoryItems = new List<string>();
            var _Data = _categoryLogic.GetAllCategoryItems();
            _CategoryItems.Add("Select Category");
            foreach (var _cat in _Data)
            {
                _CategoryItems.Add(_cat.CategoryName);
            }

            PickerCategory.Items.Clear();
            PickerCategory.Title = "Select Category";
            PickerCategory.ItemsSource = _CategoryItems;
            PickerCategory.TextColor = Color.FromHex("#000000");
            PickerCategory.SelectedIndex = 0;

            List<string> _SupplierItem = new List<string>();
            var _SupData = _supplierLogic.GetAllSupplierItems();
            _SupplierItem.Add("Select Supplier");
            foreach (var _cat in _SupData)
            {
                _SupplierItem.Add(_cat.SupplierName);
            }

            PickerSupplier.Items.Clear();
            PickerSupplier.Title = "Select Supplier";
            PickerSupplier.ItemsSource = _SupplierItem;
            PickerSupplier.TextColor = Color.FromHex("#000000");
            PickerSupplier.SelectedIndex = 0;
        }

        void AssignValues(Stocks _Item)
        {
            EntryProductName.Text = _Item.ProductName;
            EntryProductNumber.Text = _Item.ProductNumber;
            PickerCategory.SelectedItem = _Item.Category;
            EntryQuantity.Text = _Item.Quantity.ToString();
            EntryPurchaseprice.Text = _Item.PurchasePrice.ToString();
            EntrySalesPrice.Text = _Item.SalesPrice.ToString();
            PickerUnit.SelectedItem = _Item.Unit;
            PickerSupplier.SelectedItem = _Item.Supplier;
            EntryAlertQuantity.Text = _Item.AlertQuantity.ToString();
            EntryBarcode.Text = _Item.Barcode;
            EntryProductDescription.Text = _Item.ProductDescription;
            SwitchActive.IsToggled = _Item.IsActive.ToLower() == "true" ? true : false;
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string _Error = Validation();

            if (_Error == "")
            {
                Stocks _StockItem = new Stocks()
                {
                    ProductName = EntryProductName.Text,
                    ProductNumber = EntryProductNumber.Text,
                    Category = PickerCategory.SelectedItem.ToString(),
                    Quantity = int.Parse(EntryQuantity.Text),
                    PurchasePrice = int.Parse(EntryPurchaseprice.Text),
                    SalesPrice = int.Parse(EntrySalesPrice.Text),
                    Unit = PickerUnit.SelectedItem.ToString(),
                    Supplier = PickerSupplier.SelectedItem.ToString(),
                    AlertQuantity = int.Parse(EntryAlertQuantity.Text),
                    Barcode = EntryBarcode.Text,
                    ProductDescription = EntryProductDescription.Text,
                    IsActive = SwitchActive.IsToggled.ToString()
                };

                if (_EditItem == null)
                {
                    _StockItem.CreatedBy = SessionData.LoginUserName;
                    _StockItem.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _stockLogic.InsertStockItem(_StockItem);

                    var _Alert = await DisplayAlert("Success", "Stock added successfully.\nDo you want to add another Stock?", "Yes", "No");
                    if (_Alert)
                    {
                        ClearValues();
                        EntryProductName.Focus();
                    }
                    else
                    {
                        LoadList();
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    _StockItem.UniqueID = _EditItem.UniqueID;
                    _StockItem.ModifiedBy = SessionData.LoginUserName;
                    _StockItem.ModifiedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _stockLogic.UpdateStockItem(_StockItem);

                    var _Alert = await DisplayAlert("Success", "Stock updated successfully.\nDo you want to add another Stock?", "Yes", "No");
                    if (_Alert)
                    {
                        _EditItem = null;
                        ClearValues();
                        EntryProductName.Focus();
                    }
                    else
                    {
                        LoadList();
                        await Navigation.PopAsync();
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", _Error, "Ok");
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        string Validation()
        {
            string ErrorMessage = "";

            if (EntryProductName.Text.Trim() == "")
                ErrorMessage = "Product Name missing.\n";
            if (EntryProductNumber.Text.Trim() == "")
                ErrorMessage += "Product Number missing.\n";
            if (PickerCategory.SelectedIndex == 0)
                ErrorMessage += "Category missing.\n";
            if (EntryQuantity.Text.Trim() == "" || int.Parse(EntryQuantity.Text) <= 0)
                ErrorMessage += "Invalid Quantity.\n";
            if (EntryPurchaseprice.Text.Trim() == "" || int.Parse(EntryPurchaseprice.Text) <= 0)
                ErrorMessage += "Purchase price missing.\n";
            if (EntrySalesPrice.Text.Trim() == "" || int.Parse(EntrySalesPrice.Text) <= 0)
                ErrorMessage += "Sales price missing.\n";
            if (PickerUnit.SelectedIndex == 0)
                ErrorMessage += "Unit missing.\n";

            return ErrorMessage;
        }

        void ClearValues()
        {
            EntryProductName.Text = "";
            EntryProductNumber.Text = "";
            PickerCategory.SelectedIndex = 0;
            EntryQuantity.Text = "";
            EntryPurchaseprice.Text = "";
            EntrySalesPrice.Text = "";
            PickerUnit.SelectedIndex = 0;
            PickerSupplier.SelectedIndex = 0;
            EntryAlertQuantity.Text = "";
            EntryBarcode.Text = "";
            EntryProductDescription.Text = "";
            SwitchActive.IsToggled = true;
        }

        void LoadList()
        {
            var _stockItems = _stockLogic.GetAllStockItems();
            if (_stockItems != null && _stockItems.Count != 0)
            {
                StockListPage.StockAssestList.Clear();

                foreach (var _Items in _stockItems)
                    StockListPage.StockAssestList.Add(_Items);
            }
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

                        EntryBarcode.Text = result.Text;
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            };

            ImageBarcode.GestureRecognizers.Add(BarcodeImageTap);
        }

        void EntryKeyEvents()
        {
            EntryProductName.Completed += (sender, e) =>
            {
                EntryProductNumber.Focus();
            };

            EntryProductNumber.Completed += (sender, e) =>
            {
                PickerCategory.Focus();
            };
            PickerCategory.SelectedIndexChanged += (sender, e) =>
            {
                EntryQuantity.Focus();
            };
            EntryQuantity.Completed += (sender, e) =>
            {
                EntryPurchaseprice.Focus();
            };
            EntryPurchaseprice.Completed += (sender, e) =>
            {
                EntrySalesPrice.Focus();
            };
            EntrySalesPrice.Completed += (sender, e) =>
            {
                PickerUnit.Focus();
            };
            PickerUnit.SelectedIndexChanged += (sender, e) =>
            {
                PickerSupplier.Focus();
            };
            PickerSupplier.SelectedIndexChanged += (sender, e) =>
            {
                EntryAlertQuantity.Focus();
            };
            EntryAlertQuantity.Completed += (sender, e) =>
            {
                EntryBarcode.Focus();
            };
            EntryBarcode.Completed += (sender, e) =>
            {
                EntryProductDescription.Focus();
            };
        }

    }
}