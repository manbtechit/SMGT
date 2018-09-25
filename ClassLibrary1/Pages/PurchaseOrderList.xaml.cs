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
	public partial class PurchaseOrderList : ContentPage
	{
        StockLogic _stockLogic = new StockLogic();
        SupplierLogic _supplierLogic = new SupplierLogic();
       List<PurchaseOrder_Product> _CurrentPOItems;
        PurchaseOrder _EditItem;
        POLogic _POLogic = new POLogic();

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

        public PurchaseOrderList ()
		{
			InitializeComponent ();

            this.Title = "Purchase Order";

            _CurrentPOItems = new List<PurchaseOrder_Product>();

            ListProductItem.IsPullToRefreshEnabled = false;
            ListProductItem.IsRefreshing = IsBusy;
            POItemList.Clear();

            ChangeLayout(true);

            LoadList();

            LoadPicker();

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;
            ButtonAdd.Clicked += ButtonAdd_Clicked;

            var _List = SessionData._OfflineDB.GetData<PurchaseOrder>();
            var _List1 = SessionData._OfflineDB.GetData<PurchaseOrder_Product>();
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
        }

        private void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            if(EntryProductName.Text.Trim()!="")
            {
                if(EntryQuantity.Text.Trim()!="")
                {
                    PurchaseOrder_Product _Product = new PurchaseOrder_Product()
                    {
                        ProductID = EntryProductName.Text,
                        Quantity = int.Parse(EntryQuantity.Text)
                    };

                    POItemList.Add(_Product);

                    ListProductItem.ItemsSource = POItemList;
                    
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
                        OrderDate = EntryDate.Text,
                        Supplier = PickerSupplier.SelectedItem.ToString(),
                        SubTotal = double.Parse(EntrySubtotal.Text),
                        SGST = double.Parse(EntrySGST.Text),
                        CGST = double.Parse(EntryCGST.Text),
                        Discount = double.Parse(EntryDiscount.Text),
                        Total = double.Parse(EntryTotal.Text)
                    };

                    if (_EditItem == null)
                    {
                        _POLogic.InsertPurchaseOrderItems(POItemList);
                        _POLogic.InsertPurchaseOrder(_Item);

                      //  ClearValues();

                        await DisplayAlert("Success", "Purchase Order added successfully", "Ok");
                        //LoadList();
                        //ChangeLayout(true);
                    }
                    else
                    {
                        //_StockItem.UniqueID = _EditItem.UniqueID;

                        //_stockLogic.UpdateStockItem(_StockItem);

                        //ClearValues();

                        //await DisplayAlert("Success", "Stock updated successfully", "Ok");
                        //LoadList();

                        //ChangeLayout(true);
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

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
           // ChangeLayout(true);
        }

        string Validation()
        {
            string ErrorMessage = "";

            if (EntryOrderNumber.Text.Trim() == "")
                ErrorMessage = "Order Number missing.\n";
            if (EntryDate.Text.Trim() == "")
                ErrorMessage += "Order Date missing.\n";
            if (PickerSupplier.SelectedIndex == 0)
                ErrorMessage += "Supplier missing.\n";
            
            return ErrorMessage;
        }
    }
}