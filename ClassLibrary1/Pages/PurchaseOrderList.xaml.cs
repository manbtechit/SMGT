using SalesApp.BusinessClass;
using SalesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

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

            EntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            EntrySubtotal.IsEnabled = false;
            EntryCGST.IsEnabled = false;
            EntrySGST.IsEnabled = false;
            EntryTotal.IsEnabled = false;

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
        }

        void AssignValues(PurchaseOrder _Item)
        {
            EntryOrderNumber.Text = _Item.OrderNumber;
            EntryDate.Text = _Item.OrderDate;
            PickerSupplier.SelectedItem = _Item.Supplier;
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
            PickerSupplier.SelectedIndex = 0;
            PickerProduct.SelectedIndex = 0;
            EntryQuantity.Text = "";
            EntrySubtotal.Text = "";
            EntryCGST.Text = "";
            EntrySGST.Text = "";
            EntryTotal.Text = "";
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
            if(PickerProduct.SelectedIndex!=0)
            {
                if(EntryQuantity.Text.Trim()!="")
                {
                    var _PItem = _POLogic.GetStockItems(PickerProduct.SelectedItem.ToString());
                    if (_PItem != null)
                    {
                        PurchaseOrder_Product _Product = new PurchaseOrder_Product()
                        {
                            ProductID = _PItem.FirstOrDefault().UniqueID,
                            ProductName = PickerProduct.SelectedItem.ToString(),
                            Quantity = int.Parse(EntryQuantity.Text),
                            PurchasePrice = _PItem.FirstOrDefault().PurchasePrice
                        };

                        POItemList.Add(_Product);

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
                        Total = double.Parse(EntryTotal.Text)
                    };

                    if (_EditItem == null)
                    {
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

                        _POLogic.UpdateStockItem(_Item);
                        _POLogic.DeletePurchaseOrderItems(_Item.OrderNumber);
                        _POLogic.InsertPurchaseOrderItems(POItemList);

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
            else
                await DisplayAlert("Error", "Add product to PO", "Ok");
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
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
            if (PickerSupplier.SelectedIndex == 0)
                ErrorMessage += "Supplier missing.\n";
            
            return ErrorMessage;
        }
    }
}