using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseOrderPage : ContentPage
    {
        StockLogic _stockLogic = new StockLogic();
        SupplierLogic _supplierLogic = new SupplierLogic();
        List<PurchaseOrder_Product> _CurrentPOItems;
        PurchaseOrder _EditItem;
        POLogic _POLogic = new POLogic();
        ZXingScannerPage scanPage;
        PurchaseOrder_Product _EditItemProduct;

        private static ObservableCollection<PurchaseOrder> _POcollection = null;
        private static ObservableCollection<PurchaseOrder_Product> _POItemcollection = null;

        public static ObservableCollection<PurchaseOrder> POList
        {
            get
            {
                if (_POcollection == null)
                {
                    _POcollection = new ObservableCollection<PurchaseOrder>();
                }
                return _POcollection;
            }
            set { _POcollection = value; }
        }

        public static ObservableCollection<PurchaseOrder_Product> POItemList
        {
            get
            {
                if (_POItemcollection == null)
                {
                    _POItemcollection = new ObservableCollection<PurchaseOrder_Product>();
                }
                return _POItemcollection;
            }
            set { _POItemcollection = value; }
        }

        public PurchaseOrderPage(string _view)
        {
            InitializeComponent();

            this.Title = "";
            this.ClassId = "Purchase Order";
            this.BackgroundColor = Color.White;
            _CurrentPOItems = new List<PurchaseOrder_Product>();

            ListProductItem.IsPullToRefreshEnabled = false;
            ListProductItem.IsRefreshing = IsBusy;
            POItemList.Clear();

            if (_view == "Add")
                ChangeLayout(false);
            else
                ChangeLayout(true);

            POList.Clear();
            POItemList.Clear();
            ListProductItem.ItemsSource = null;
            ListProduct.ItemsSource = null;

            LoadList();

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;
            ButtonAdd.Clicked += ButtonAdd_Clicked;

            var _List = SessionData._OfflineDB.GetData<PurchaseOrder>();
            var _List1 = SessionData._OfflineDB.GetData<PurchaseOrder_Product>();

            PickerOrderDate.Date = DateTime.Now.Date;

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            EntrySubtotal.IsEnabled = false;
            EntryCGST.IsEnabled = false;
            EntrySGST.IsEnabled = false;
            EntryTotal.IsEnabled = false;
            EntryOrderNumber.IsEnabled = false;

            ListProduct.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (PurchaseOrder)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            ListProductItem.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (PurchaseOrder_Product)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                PickerProduct.SelectedItem = _Item.ProductName;
                EntryQuantity.Text = _Item.Quantity.ToString();

                _EditItemProduct = _Item;
            };

            _EditItemProduct = null;

            BarcodeInit();

            GenerateID();

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                OverallStack.WidthRequest = Utilities.TabletWidth;
            }
        }

        void GenerateID()
        {
            var _POItems = _POLogic.GetAllPurchaseOrderNoStatus();
            if (_POItems != null && _POItems.Count != 0)
            {
                var _Number = _POItems.OrderBy(i => i.OrderNumber).LastOrDefault().OrderNumber;
                int _Count = int.Parse(_Number) + 1;

                EntryOrderNumber.Text = _Count.ToString();
            }
            else
                EntryOrderNumber.Text = "1001";
        }

        void AssignValues(PurchaseOrder _Item)
        {
            EntryOrderNumber.Text = _Item.OrderNumber;
            PickerOrderDate.Date = Convert.ToDateTime(_Item.OrderDate);
            PickerSupplier.SelectedItem = _Item.Supplier;
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";

            EntrySubtotal.Text = _Item.SubTotal.ToString();
            EntryCGST.Text = _Item.CGST.ToString();
            EntrySGST.Text = _Item.SGST.ToString();
            EntryTotal.Text = _Item.Total.ToString();

            var _Prod = _POLogic.GetAllPurchaseProduct(_Item.OrderNumber);
            if (_Prod != null && _Prod.Count != 0)
            {
                POItemList.Clear();

                foreach (var _itm in _Prod)
                {
                    POItemList.Add(_itm);
                }
                ListProductItem.ItemsSource = POItemList;
            }
        }

        void ChangeLayout(bool _Value)
        {
            ListContent.IsVisible = _Value;
            FormContent.IsVisible = !_Value;

            if (_Value)
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

        void ClearValues()
        {
            EntryOrderNumber.Text = "";
            PickerOrderDate.Date = DateTime.Now.Date;
            PickerSupplier.SelectedIndex = 0;
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";
            EntrySubtotal.Text = "";
            EntryCGST.Text = "";
            EntrySGST.Text = "";
            EntryTotal.Text = "";

            POItemList.Clear();
            ListProductItem.ItemsSource = null;
        }

        void LoadPicker()
        {
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

            List<string> _ProductItem = new List<string>();
            var _ProdData = _stockLogic.GetActiveStockItems();
            _ProductItem.Add("Select Product");
            foreach (var _cat in _ProdData)
            {
                _ProductItem.Add(_cat.ProductName);
            }

            PickerProduct.Items.Clear();
            PickerProduct.Title = "Select Product";
            PickerProduct.ItemsSource = _ProductItem;
            PickerProduct.TextColor = Color.FromHex("#000000");
            PickerProduct.SelectedIndex = 0;
        }

        void LoadList()
        {
            var _POItems = _POLogic.GetAllPurchaseOrder();
            if (_POItems != null && _POItems.Count != 0)
            {
                POList.Clear();

                foreach (var _Items in _POItems)
                    POList.Add(_Items);

                ListProduct.ItemsSource = POList;
            }
            else
            {
                POList.Clear();
                ListProduct.ItemsSource = null;
            }
        }

        private void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            if (_EditItemProduct == null)
            {
                if (PickerProduct.SelectedIndex != 0)
                {
                    if (EntryQuantity.Text.Trim() != "")
                    {
                        var _PItem = _POLogic.GetStockItems(PickerProduct.SelectedItem.ToString());
                        if (_PItem != null)
                        {
                            AddBarcodeProduct(_PItem.FirstOrDefault(), "");
                        }
                    }
                }
            }
            else
            {
                var POItem = POItemList.Where(i => i.UniqueID == _EditItemProduct.UniqueID);
                if (POItem.Count() != 0)
                {
                    _EditItemProduct.Quantity = int.Parse(EntryQuantity.Text);

                    POItemList.Remove(POItem.FirstOrDefault());
                    POItemList.Add(_EditItemProduct);

                    _EditItemProduct = null;
                    ListProductItem.ItemsSource = null;
                    ListProductItem.ItemsSource = POItemList;

                    double subtotal = 0;
                    foreach (var _prod in POItemList)
                    {
                        subtotal = subtotal + (_prod.Quantity * _prod.PurchasePrice);
                    }

                    EntrySubtotal.Text = subtotal.ToString();

                    double CGST = (subtotal * SessionData.CGST) / 100;
                    double SGST = (subtotal * SessionData.SGST) / 100;

                    EntryCGST.Text = CGST.ToString();
                    EntrySGST.Text = SGST.ToString();

                    double Total = subtotal + CGST + SGST;
                    EntryTotal.Text = Total.ToString();

                    PickerProduct.SelectedIndex = 0;
                    EntryQuantity.Text = "";
                }
            }
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (POItemList.Count != 0)
            {
                string _Error = Validation();

                if (_Error == "")
                {
                    PurchaseOrder _Item = new PurchaseOrder()
                    {
                        OrderNumber = EntryOrderNumber.Text,
                        OrderDate = PickerOrderDate.Date.ToString("dd/MM/yyyy"),
                        Supplier = PickerSupplier.SelectedItem.ToString(),
                        SubTotal = double.Parse(EntrySubtotal.Text),
                        SGST = double.Parse(EntrySGST.Text),
                        CGST = double.Parse(EntryCGST.Text),
                        Total = double.Parse(EntryTotal.Text),
                        Status = PurchaseOrderStatus.Open.ToString()
                    };

                    if (_EditItem == null)
                    {
                        _Item.CreatedBy = SessionData.LoginUserName;
                        _Item.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                        _POLogic.InsertPurchaseOrderItems(POItemList);
                        _POLogic.InsertPurchaseOrder(_Item);

                        ClearValues();

                        await DisplayAlert("Success", "Purchase Order added successfully", "Ok");
                        LoadList();
                        ChangeLayout(true);
                    }
                    else
                    {
                        _Item.UniqueID = _EditItem.UniqueID;
                        _Item.ModifiedBy = SessionData.LoginUserName;
                        _Item.ModifiedDate = DateTime.Now.ToString("dd/MM/yyyy");

                        _POLogic.UpdateStockItem(_Item);
                        _POLogic.DeletePurchaseOrderItems(_Item.OrderNumber);
                        _POLogic.InsertPurchaseOrderItems(POItemList);

                        ClearValues();

                        await DisplayAlert("Success", "Purchase Order updated successfully", "Ok");
                        LoadList();

                        ChangeLayout(true);
                    }
                }
                else
                {
                    await DisplayAlert("Error", _Error, "Ok");
                }
            }
            else
                await DisplayAlert("Error", "Add product to PO", "Ok");
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout(true);
        }

        string Validation()
        {
            string ErrorMessage = "";

            if (EntryOrderNumber.Text.Trim() == "")
                ErrorMessage = "Order Number missing.\n";
            if (PickerSupplier.SelectedIndex == 0)
                ErrorMessage += "Supplier missing.\n";

            return ErrorMessage;
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
                        AddBarcodeProduct(null, result.Text);
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            };

            ImageBarcode.GestureRecognizers.Add(BarcodeImageTap);
        }

        void AddBarcodeProduct(Stocks _StItem, string Barcode)
        {
            if (_StItem == null)
            {
                _StItem = _POLogic.GetStockItembyBarcode(Barcode);
                EntryQuantity.Text = "1";
            }

            if (_StItem != null)
            {
                PurchaseOrder_Product _Product = new PurchaseOrder_Product()
                {
                    ProductID = _StItem.UniqueID,
                    ProductName = _StItem.ProductName,
                    Quantity = int.Parse(EntryQuantity.Text),
                    PurchasePrice = _StItem.PurchasePrice,
                    Barcode = _StItem.Barcode,
                    OrderNumber = EntryOrderNumber.Text
                };

                if (POItemList != null && POItemList.Count != 0)
                {
                    var POItem = POItemList.Where(i => i.ProductID == _StItem.UniqueID);
                    if (POItem.Count() != 0)
                    {
                        int _qty = POItem.FirstOrDefault().Quantity + int.Parse(EntryQuantity.Text);

                        _Product.Quantity = _qty;
                        POItemList.Remove(POItem.FirstOrDefault());
                        POItemList.Add(_Product);
                    }
                    else
                    {
                        POItemList.Add(_Product);
                    }
                }
                else
                {
                    POItemList.Add(_Product);
                }

                ListProductItem.ItemsSource = POItemList;

                double subtotal = 0;
                foreach (var _prod in POItemList)
                {
                    subtotal = subtotal + (_prod.Quantity * _prod.PurchasePrice);
                }

                EntrySubtotal.Text = subtotal.ToString();

                double CGST = (subtotal * SessionData.CGST) / 100;
                double SGST = (subtotal * SessionData.SGST) / 100;

                EntryCGST.Text = CGST.ToString();
                EntrySGST.Text = SGST.ToString();

                double Total = subtotal + CGST + SGST;
                EntryTotal.Text = Total.ToString();

                PickerProduct.SelectedIndex = 0;
                EntryQuantity.Text = "";
            }
            else
            {
                DisplayAlert("Error", "Product not found.", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Application.Current.MainPage = new MenuMaster();
            return true;
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            if (PickerOrderDate.Date > DateTime.Now.Date)
            {
                DisplayAlert("Error", "Date cannont be future date", "Ok");
                PickerOrderDate.Date = DateTime.Now.Date;
            }
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _POLogic.GetSearchItems(EntrySearch.Text);
            if (_Filter != null)
            {
                POList.Clear();

                foreach (var _Items in _Filter)
                    POList.Add(_Items);

                ListProduct.ItemsSource = POList;
            }
            else
            {
                ListProduct.ItemsSource = null;
            }
        }

    }
}