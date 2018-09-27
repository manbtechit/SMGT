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
	public partial class SalesPage : ContentPage
	{
        StockLogic _stockLogic = new StockLogic();
        List<SalesOrder_Product> _CurrentPOItems;
        SalesOrder _EditItem;
        SalesLogic _SalesLogic = new SalesLogic();
        ZXingScannerPage scanPage;

        private static ObservableCollection<SalesOrder> _Salecollection = null;
        private static ObservableCollection<SalesOrder_Product> _SaleItemcollection = null;

        public static ObservableCollection<SalesOrder> SaleList
        {
            get
            {
                if (_Salecollection == null)
                {
                    _Salecollection = new ObservableCollection<SalesOrder>();
                }
                return _Salecollection;
            }
            set { _Salecollection = value; }
        }

        public static ObservableCollection<SalesOrder_Product> SaleItemList
        {
            get
            {
                if (_SaleItemcollection == null)
                {
                    _SaleItemcollection = new ObservableCollection<SalesOrder_Product>();
                }
                return _SaleItemcollection;
            }
            set { _SaleItemcollection = value; }
        }

        public SalesPage()
        {
            InitializeComponent();

            this.Title = "Sales Order";

            _CurrentPOItems = new List<SalesOrder_Product>();

            ListProductItem.IsPullToRefreshEnabled = false;
            ListProductItem.IsRefreshing = IsBusy;
            SaleList.Clear();

            ChangeLayout(true);

            LoadList();

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;
            ButtonAdd.Clicked += ButtonAdd_Clicked;

            EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            EntrySubtotal.IsEnabled = false;
            EntryCGST.IsEnabled = false;
            EntrySGST.IsEnabled = false;
            EntryTotal.IsEnabled = false;

            ListProduct.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (SalesOrder)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            BarcodeInit();
        }

        void AssignValues(SalesOrder _Item)
        {
            EntryOrderNumber.Text = _Item.OrderNumber;
            EntryDate.Text = _Item.OrderDate;
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";

            EntrySubtotal.Text = _Item.SubTotal.ToString();
            EntryCGST.Text = _Item.CGST.ToString();
            EntrySGST.Text = _Item.SGST.ToString();
            EntryTotal.Text = _Item.Total.ToString();
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
            EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";
            EntrySubtotal.Text = "";
            EntryCGST.Text = "";
            EntrySGST.Text = "";
            EntryTotal.Text = "";
        }

        void LoadPicker()
        {
            List<string> _ProductItem = new List<string>();
            var _ProdData = _stockLogic.GetAllStockItems();
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
            var _POItems = _SalesLogic.GetAllSalesOrder();
            if (_POItems != null && _POItems.Count != 0)
            {
                SaleList.Clear();

                foreach (var _Items in _POItems)
                    SaleList.Add(_Items);

                ListProduct.ItemsSource = SaleList;
            }
        }

        private void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            if (PickerProduct.SelectedIndex != 0)
            {
                if (EntryQuantity.Text.Trim() != "")
                {
                    var _PItem = _SalesLogic.GetStockItems(PickerProduct.SelectedItem.ToString());
                    if (_PItem != null)
                    {
                        AddBarcodeProduct(_PItem.FirstOrDefault(), "");
                    }
                }
            }
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (SaleItemList.Count != 0)
            {
                string _Error = Validation();

                if (_Error == "")
                {
                    SalesOrder _Item = new SalesOrder()
                    {
                        OrderNumber = EntryOrderNumber.Text,
                        OrderDate = EntryDate.Text,
                        SubTotal = double.Parse(EntrySubtotal.Text),
                        SGST = double.Parse(EntrySGST.Text),
                        CGST = double.Parse(EntryCGST.Text),
                        Total = double.Parse(EntryTotal.Text)
                    };

                    if (_EditItem == null)
                    {
                        _SalesLogic.InsertSalesOrderItems(SaleItemList);
                        _SalesLogic.InsertSalesOrder(_Item);

                        ClearValues();

                        await DisplayAlert("Success", "Sales added successfully", "Ok");
                        LoadList();
                        ChangeLayout(true);
                    }
                    else
                    {
                        _Item.UniqueID = _EditItem.UniqueID;

                        _SalesLogic.UpdateSalesOrder(_Item);
                        _SalesLogic.DeleteSalesOrderItems(_Item.OrderNumber);
                        _SalesLogic.InsertSalesOrderItems(SaleItemList);

                        ClearValues();

                        await DisplayAlert("Success", "Sales updated successfully", "Ok");
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
                await DisplayAlert("Error", "Add product to Sales", "Ok");
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
            if (EntryDate.Text.Trim() == "")
                ErrorMessage += "Order Date missing.\n";

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
                _StItem = _SalesLogic.GetStockItembyBarcode(Barcode);
                EntryQuantity.Text = "1";
            }

            if (_StItem != null)
            {
                SalesOrder_Product _Product = new SalesOrder_Product()
                {
                    ProductID = _StItem.UniqueID,
                    ProductName = _StItem.ProductName,
                    Quantity = int.Parse(EntryQuantity.Text),
                    SalesPrice = _StItem.SalesPrice,
                    Barcode = _StItem.Barcode
                };

                if (SaleItemList != null && SaleItemList.Count != 0)
                {
                    var POItem = SaleItemList.Where(i => i.ProductID == _StItem.UniqueID);
                    if (POItem.Count() != 0)
                    {
                        int _qty = POItem.FirstOrDefault().Quantity + int.Parse(EntryQuantity.Text);

                        _Product.Quantity = _qty;
                        SaleItemList.Remove(POItem.FirstOrDefault());
                        SaleItemList.Add(_Product);
                    }
                    else
                    {
                        SaleItemList.Add(_Product);
                    }
                }
                else
                {
                    SaleItemList.Add(_Product);
                }

                ListProductItem.ItemsSource = SaleItemList;

                double subtotal = 0;
                foreach (var _prod in SaleItemList)
                {
                    subtotal = subtotal + (_prod.Quantity * _prod.SalesPrice);
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
    }
}