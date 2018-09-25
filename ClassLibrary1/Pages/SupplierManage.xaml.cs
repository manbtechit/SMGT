﻿using SalesApp.BusinessClass;
using SalesApp.Model;
using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SupplierManage : ContentPage
	{
        SupplierLogic _supplierLogic = new SupplierLogic();
        Supplier _EditItem;

        private static ObservableCollection<Supplier> _suppliercollection = null;

        public static ObservableCollection<Supplier> SupplierAssestList
        {
            get
            {
                if (_suppliercollection == null)
                {
                    _suppliercollection = new ObservableCollection<Supplier>();
                }
                return _suppliercollection;
            }
            set { _suppliercollection = value; }
        }

        public SupplierManage()
        {
            InitializeComponent();

            this.Title = "Supplier";

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;

            EntrySupplierName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            EntryCompanyNumber.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            
            ListSupplier.IsPullToRefreshEnabled = false;
            ListSupplier.IsRefreshing = IsBusy;
            ChangeLayout(true);

            ListSupplier.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (Supplier)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            LoadList();

        }

        void AssignValues(Supplier _Item)
        {
            EntrySupplierName.Text = _Item.SupplierName;
            EntryCompanyNumber.Text = _Item.CompanyName;
            EntryPhone.Text = _Item.Phone;
            EntryMobile.Text = _Item.Mobile;
            EntryEmail.Text = _Item.Email;
            EntryBillingAddress.Text = _Item.BillingAddress;
            EntryBillingCity.Text = _Item.BillingCity;
            EntryBillingState.Text = _Item.BillingState;
            EntryBillingZipcode.Text = _Item.BillingZipCode;
            EntryNotes.Text = _Item.Notes;            
            SwitchActive.IsToggled = _Item.IsActive.ToLower() == "true" ? true : false;
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

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string _Error = Validation();

            if (_Error == "")
            {
                Supplier _SupplierItem = new Supplier()
                {
                    SupplierName = EntrySupplierName.Text,
                    CompanyName = EntryCompanyNumber.Text,
                    Phone = EntryPhone.Text,
                    Mobile = EntryMobile.Text,
                    Email = EntryEmail.Text,
                    BillingAddress = EntryBillingAddress.Text,
                    BillingCity = EntryBillingCity.Text,
                    BillingState = EntryBillingState.Text,
                    BillingZipCode = EntryBillingZipcode.Text,
                    Notes = EntryNotes.Text,
                    IsActive = SwitchActive.IsToggled.ToString()
                };

                if (_EditItem == null)
                {
                    _supplierLogic.InsertSupplierItem(_SupplierItem);

                    ClearValues();

                    await DisplayAlert("Success", "Supplier added successfully", "Ok");
                    LoadList();
                    ChangeLayout(true);
                }
                else
                {
                    _SupplierItem.UniqueID = _EditItem.UniqueID;

                    _supplierLogic.UpdateSupplierItem(_SupplierItem);

                    ClearValues();

                    await DisplayAlert("Success", "Supplier updated successfully", "Ok");
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
            var _supplieritem = _supplierLogic.GetAllSupplierItems();
            if (_supplieritem != null && _supplieritem.Count != 0)
            {
                SupplierAssestList.Clear();

                foreach (var _Items in _supplieritem)
                    SupplierAssestList.Add(_Items);

                ListSupplier.ItemsSource = SupplierAssestList;
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout(true);
        }

        string Validation()
        {
            string ErrorMessage = "";

            if (EntrySupplierName.Text.Trim() == "")
                ErrorMessage = "Supplier Name missing.\n";
            if (EntryCompanyNumber.Text.Trim() == "")
                ErrorMessage += "Company Name missing.\n";
            if (EntryMobile.Text.Trim()=="")
                ErrorMessage += "Mobile missing.\n";
            if (EntryBillingAddress.Text.Trim() == "" )
                ErrorMessage += "Address missing.\n";
            if (EntryBillingCity.Text.Trim() == "" )
                ErrorMessage += "City missing.\n";
            if (EntryBillingState.Text.Trim() == "")
                ErrorMessage += "State missing.\n";
            if (EntryBillingZipcode.Text.Trim()=="")
                ErrorMessage += "Zipcode missing.\n";

            return ErrorMessage;
        }

        void ClearValues()
        {
            EntrySupplierName.Text = "";
            EntryCompanyNumber.Text = "";
            EntryPhone.Text = "";
            EntryMobile.Text = "";
            EntryEmail.Text = "";
            EntryBillingAddress.Text = "";
            EntryBillingCity.Text = "";
            EntryBillingState.Text = "";
            EntryBillingZipcode.Text = "";
            EntryNotes.Text = "";
            SwitchActive.IsToggled = true;
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _supplierLogic.SearchSupplierItems(EntrySearch.Text);
            if (_Filter != null)
            {
                SupplierAssestList.Clear();

                foreach (var _Items in _Filter)
                    SupplierAssestList.Add(_Items);

                ListSupplier.ItemsSource = SupplierAssestList;
            }
            else
            {
                ListSupplier.ItemsSource = null;
            }
        }

    }
}