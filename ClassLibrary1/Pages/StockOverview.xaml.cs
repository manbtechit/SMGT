using SalesApp.BusinessClass;
using SalesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockOverview : ContentPage
    {
        StockLogic _stockLogic = new StockLogic();
        CategoryLogic _categoryLogic = new CategoryLogic();
        Stocks _EditItem;
        SupplierLogic _supplierLogic = new SupplierLogic();

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

        public StockOverview()
        {
            InitializeComponent();

            this.Title = "Stock Overview";

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;

            EntryProductName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            EntryProductNumber.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            EntryProductDescription.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);

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

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            LoadList();
        }

        void LoadPicker()
        {
            List<string> _UnitItems = new List<string>();

            _UnitItems.Add("Select Unit");
            _UnitItems.Add("Kg");
            _UnitItems.Add("Gms");
            _UnitItems.Add("Ltr");
            _UnitItems.Add("Piece");

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

        void ChangeLayout(bool _Value)
        {
            ListContent.IsVisible = _Value;
            FormContent.IsVisible = !_Value;

            if(_Value)
            {
                ToolbarItems.Add(new ToolbarItem("Add", "Add.png", () =>
                {
                    _EditItem = null;
                    ClearValues();
                    ChangeLayout(false);
                }));
            }
            else
            {
                ToolbarItems.Clear();
            }
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
                    _stockLogic.InsertStockItem(_StockItem);

                    ClearValues();

                    await DisplayAlert("Success", "Stock added successfully", "Ok");
                    LoadList();
                    ChangeLayout(true);
                }
                else
                {
                    _StockItem.UniqueID = _EditItem.UniqueID;

                    _stockLogic.UpdateStockItem(_StockItem);

                    ClearValues();

                    await DisplayAlert("Success", "Stock updated successfully", "Ok");
                    LoadList();

                    ChangeLayout(true);
                }
            }
            else
            {
                await DisplayAlert("Error", _Error, "Ok");
            }
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

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout(true);
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

    }
}