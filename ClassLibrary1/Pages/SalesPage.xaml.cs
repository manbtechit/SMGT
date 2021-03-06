﻿using System;
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
        SalesOrder_Product _EditItemProduct;

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

        public SalesPage(string _view)
        {
            InitializeComponent();

            this.Title = "";
            this.ClassId = "Sales Order";
            this.BackgroundColor = Color.White;
            _CurrentPOItems = new List<SalesOrder_Product>();

            ListProductItem.IsPullToRefreshEnabled = false;
            ListProductItem.IsRefreshing = IsBusy;
            SaleList.Clear();

            if (_view == "Add")
                ChangeLayout(false);
            else
                ChangeLayout(true);

            SaleList.Clear();
            SaleItemList.Clear();
            ListProductItem.ItemsSource = null;
            ListProduct.ItemsSource = null;

            LoadList();

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;
            ButtonAdd.Clicked += ButtonAdd_Clicked;

            PickerOrderDate.Date = DateTime.Now.Date;

            EntrySubtotal.IsEnabled = false;
            EntryCGST.IsEnabled = false;
            EntrySGST.IsEnabled = false;
            EntryTotal.IsEnabled = false;
            EntryOrderNumber.IsEnabled = false;

            EntrySearch.TextChanged += EntrySearch_TextChanged;

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

            ListProductItem.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (SalesOrder_Product)args.SelectedItem;
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
            var _POItems = _SalesLogic.GetAllSalesOrder();
            if (_POItems != null && _POItems.Count != 0)
            {
                var _Number = _POItems.OrderBy(i => i.OrderNumber).LastOrDefault().OrderNumber;
                int _Count = int.Parse(_Number) + 1;

                EntryOrderNumber.Text = _Count.ToString();
            }
            else
                EntryOrderNumber.Text = "1001";
        }

        void AssignValues(SalesOrder _Item)
        {
            EntryOrderNumber.Text = _Item.OrderNumber;
            PickerOrderDate.Date =Convert.ToDateTime(_Item.OrderDate);
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";

            EntrySubtotal.Text = _Item.SubTotal.ToString();
            EntryCGST.Text = _Item.CGST.ToString();
            EntrySGST.Text = _Item.SGST.ToString();
            EntryTotal.Text = _Item.Total.ToString();

            var _Prod = _SalesLogic.GetAllSalesProduct(_Item.OrderNumber);
            if (_Prod != null && _Prod.Count != 0)
            {
                SaleItemList.Clear();

                foreach (var _itm in _Prod)
                {
                    SaleItemList.Add(_itm);
                }
                ListProductItem.ItemsSource = SaleItemList;
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

                    GenerateID();
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
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";
            EntrySubtotal.Text = "";
            EntryCGST.Text = "";
            EntrySGST.Text = "";
            EntryTotal.Text = "";

            SaleItemList.Clear();
            ListProductItem.ItemsSource = null;
        }

        void LoadPicker()
        {
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
            var _POItems = _SalesLogic.GetAllSalesOrder();
            if (_POItems != null && _POItems.Count != 0)
            {
                SaleList.Clear();

                foreach (var _Items in _POItems)
                    SaleList.Add(_Items);

                ListProduct.ItemsSource = SaleList;
            }
            else
            {
                SaleList.Clear();
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
                        var _PItem = _SalesLogic.GetStockItems(PickerProduct.SelectedItem.ToString());
                        if (_PItem != null)
                        {
                            AddBarcodeProduct(_PItem.FirstOrDefault(), "");
                        }
                    }
                }
            }
            else
            {
                var POItem = SaleItemList.Where(i => i.UniqueID == _EditItemProduct.UniqueID);
                if (POItem.Count() != 0)
                {
                    var _PItem = _SalesLogic.GetStockItems(POItem.FirstOrDefault().ProductName).FirstOrDefault();
                    if (_PItem != null)
                    {
                        if (_PItem.Quantity >= int.Parse(EntryQuantity.Text))
                        {
                            ListProductItem.ItemsSource = null;

                            _EditItemProduct.Quantity = int.Parse(EntryQuantity.Text);

                            SaleItemList.Remove(POItem.FirstOrDefault());
                            SaleItemList.Add(_EditItemProduct);

                            _EditItemProduct = null;
                           
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
                            DisplayAlert("Error", "Quantity available : " + _PItem.Quantity + " only", "OK");
                        }
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
                        OrderDate = PickerOrderDate.Date.ToString("dd/MM/yyyy"),
                        SubTotal = double.Parse(EntrySubtotal.Text),
                        SGST = double.Parse(EntrySGST.Text),
                        CGST = double.Parse(EntryCGST.Text),
                        Total = double.Parse(EntryTotal.Text)
                    };

                    if (_EditItem == null)
                    {
                        _Item.CreatedBy = SessionData.LoginUserName;
                        _Item.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                        foreach (var _itm in SaleItemList)
                        {
                            _SalesLogic.UpdateStocks(_itm.ProductID, _itm.Quantity);
                        }

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
                        _Item.ModifiedBy = SessionData.LoginUserName;
                        _Item.ModifiedDate = DateTime.Now.ToString("dd/MM/yyyy");

                        foreach (var _itm in SaleItemList)
                        {
                            _SalesLogic.UpdateStocks(_itm.ProductID, _itm.Quantity);
                        }

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
                if (_StItem.Quantity >= int.Parse(EntryQuantity.Text))
                {
                    SalesOrder_Product _Product = new SalesOrder_Product()
                    {
                        ProductID = _StItem.UniqueID,
                        ProductName = _StItem.ProductName,
                        Quantity = int.Parse(EntryQuantity.Text),
                        SalesPrice = _StItem.SalesPrice,
                        Barcode = _StItem.Barcode,
                        OrderNumber = EntryOrderNumber.Text
                    };

                    ListProductItem.ItemsSource = null;

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
                    DisplayAlert("Error", "Quantity available : " + _StItem.Quantity + " only", "OK");
                }
            }
            else
                DisplayAlert("Error", "Product not found.", "OK");
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            Application.Current.MainPage = new MenuMaster();
            return true;
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _SalesLogic.GetSearchItems(EntrySearch.Text);
            if (_Filter != null)
            {
                SaleList.Clear();

                foreach (var _Items in _Filter)
                    SaleList.Add(_Items);

                ListProduct.ItemsSource = SaleList;
            }
            else
            {
                ListProduct.ItemsSource = null;
            }
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            if (PickerOrderDate.Date > DateTime.Now.Date)
            {
                DisplayAlert("Error", "Date cannont be future date", "Ok");
                PickerOrderDate.Date = DateTime.Now.Date;
            }
        }

    }
}