﻿using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerPage : ContentPage
    {
        CustomerLogic _CustomerLogic = new CustomerLogic();
        Customer _EditItem;

        private static ObservableCollection<Customer> _Customercollection = null;

        public static ObservableCollection<Customer> CustomerAssestList
        {
            get
            {
                if (_Customercollection == null)
                {
                    _Customercollection = new ObservableCollection<Customer>();
                }
                return _Customercollection;
            }
            set { _Customercollection = value; }
        }

        public CustomerPage()
        {
            InitializeComponent();

            this.Title = "";
            this.ClassId = "Customer";
            NavigationPage.SetHasNavigationBar(this, false);
            this.BackgroundColor = Color.White;
            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;

            EntryCustomerName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);

            ListCustomer.IsPullToRefreshEnabled = false;
            ListCustomer.IsRefreshing = IsBusy;
            ChangeLayout(true);

            ListCustomer.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (Customer)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            LoadList();

            EntryKeyEvents();

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                ListContent.WidthRequest = Utilities.TabletWidth;
                FormContent.WidthRequest = Utilities.TabletWidth;
            }
        }

        void AssignValues(Customer _Item)
        {
            EntryCustomerName.Text = _Item.CustomerName;
            EntryMobile.Text = _Item.Mobile;
            EntryEmail.Text = _Item.Email;
            EntryBillingAddress.Text = _Item.BillingAddress;
            EntryBillingCity.Text = _Item.BillingCity;
            EntryBillingState.Text = _Item.BillingState;
            EntryBillingZipcode.Text = _Item.BillingZipCode;
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
                Customer _custItem = new Customer()
                {
                    CustomerName = EntryCustomerName.Text,
                    Mobile = EntryMobile.Text,
                    Email = EntryEmail.Text,
                    BillingAddress = EntryBillingAddress.Text,
                    BillingCity = EntryBillingCity.Text,
                    BillingState = EntryBillingState.Text,
                    BillingZipCode = EntryBillingZipcode.Text,
                    IsActive = SwitchActive.IsToggled.ToString()
                };

                if (_EditItem == null)
                {
                    _custItem.CreatedBy = SessionData.LoginUserName;
                    _custItem.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _CustomerLogic.InsertCustomerItem(_custItem);

                    ClearValues();

                    var _Alert = await DisplayAlert("Success", "Customer added successfully.\nDo you want to add another Customer?", "Yes", "No");
                    if (_Alert)
                    {
                        ClearValues();
                        EntryCustomerName.Focus();
                    }
                    else
                    {
                        LoadList();
                        ChangeLayout(true);
                    }
                }
                else
                {
                    _custItem.UniqueID = _EditItem.UniqueID;
                    _custItem.ModifiedBy = SessionData.LoginUserName;
                    _custItem.ModifiedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _CustomerLogic.UpdateCustomerItem(_custItem);

                    ClearValues();

                    var _Alert = await DisplayAlert("Success", "Customer updated successfully.\nDo you want to add another Customer?", "Yes", "No");
                    if (_Alert)
                    {
                        _EditItem = null;
                        ClearValues();
                        EntryCustomerName.Focus();
                    }
                    else
                    {
                        LoadList();
                        ChangeLayout(true);
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", _Error, "Ok");
            }
        }

        void LoadList()
        {
            var _item = _CustomerLogic.GetAllCustomerItems();
            if (_item != null && _item.Count != 0)
            {
                CustomerAssestList.Clear();

                foreach (var _Items in _item)
                    CustomerAssestList.Add(_Items);

                ListCustomer.ItemsSource = CustomerAssestList;
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout(true);
        }

        string Validation()
        {
            string ErrorMessage = "";

            if (EntryCustomerName.Text.Trim() == "")
                ErrorMessage = "Customer Name missing.\n";
            if (EntryMobile.Text.Trim() == "")
                ErrorMessage += "Mobile missing.\n";
            if (EntryBillingAddress.Text.Trim() == "")
                ErrorMessage += "Address missing.\n";
            if (EntryBillingCity.Text.Trim() == "")
                ErrorMessage += "City missing.\n";
            if (EntryBillingState.Text.Trim() == "")
                ErrorMessage += "State missing.\n";
            if (EntryBillingZipcode.Text.Trim() == "")
                ErrorMessage += "Zipcode missing.\n";

            return ErrorMessage;
        }

        void ClearValues()
        {
            EntryCustomerName.Text = "";
            EntryMobile.Text = "";
            EntryEmail.Text = "";
            EntryBillingAddress.Text = "";
            EntryBillingCity.Text = "";
            EntryBillingState.Text = "";
            EntryBillingZipcode.Text = "";
            SwitchActive.IsToggled = true;
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _CustomerLogic.SearchCustomerItems(EntrySearch.Text);
            if (_Filter != null)
            {
                CustomerAssestList.Clear();

                foreach (var _Items in _Filter)
                    CustomerAssestList.Add(_Items);

                ListCustomer.ItemsSource = CustomerAssestList;
            }
            else
            {
                ListCustomer.ItemsSource = null;
            }
        }

        void EntryKeyEvents()
        {
            EntryCustomerName.Completed += (sender, e) =>
            {
                EntryMobile.Focus();
            };
            EntryMobile.Completed += (sender, e) =>
            {
                EntryEmail.Focus();
            };
            EntryEmail.Completed += (sender, e) =>
            {
                EntryBillingAddress.Focus();
            };
            EntryBillingAddress.Completed += (sender, e) =>
            {
                EntryBillingCity.Focus();
            };
            EntryBillingCity.Completed += (sender, e) =>
            {
                EntryBillingState.Focus();
            };
            EntryBillingState.Completed += (sender, e) =>
            {
                EntryBillingZipcode.Focus();
            };
        }
    }
}