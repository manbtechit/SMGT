﻿using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        CategoryLogic _categoryLogic = new CategoryLogic();
        Category _EditItem;

        private static ObservableCollection<Category> _categorycollection = null;

        public static ObservableCollection<Category> CategoryAssestList
        {
            get
            {
                if (_categorycollection == null)
                {
                    _categorycollection = new ObservableCollection<Category>();
                }
                return _categorycollection;
            }
            set { _categorycollection = value; }
        }

        public CategoryPage()
        {
            InitializeComponent();

            this.Title = "";
            this.ClassId = "Category";
            this.BackgroundColor = Color.White;
            NavigationPage.SetHasNavigationBar(this, false);

            ListCategory.IsPullToRefreshEnabled = false;
            ListCategory.IsRefreshing = IsBusy;

            ListCategory.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem == null)
                    return;

                var _Item = (Category)args.SelectedItem;
                ((ListView)sender).SelectedItem = null;

                _EditItem = _Item;
                AssignValues(_Item);
                ChangeLayout(false);
            };

            EntrySearch.TextChanged += EntrySearch_TextChanged;

            ButtonSave.Clicked += ButtonSave_Clicked;
            ButtonCancel.Clicked += ButtonCancel_Clicked;

            EntryCategoryName.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);

            LoadList();

            ChangeLayout(true);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                ListContent.WidthRequest = Utilities.TabletWidth;
                FormContent.WidthRequest = Utilities.TabletWidth;
            }
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _Filter = _categoryLogic.SearchCategoryItems(EntrySearch.Text);
            if (_Filter != null)
            {
                CategoryAssestList.Clear();

                foreach (var _Items in _Filter)
                    CategoryAssestList.Add(_Items);

                ListCategory.ItemsSource = CategoryAssestList;
            }
            else
            {
                ListCategory.ItemsSource = null;
            }
        }

        void LoadList()
        {
            var _CatItems = _categoryLogic.GetAllCategoryItems();
            if (_CatItems != null && _CatItems.Count != 0)
            {
                CategoryAssestList.Clear();

                foreach (var _Items in _CatItems)
                    CategoryAssestList.Add(_Items);

                ListCategory.ItemsSource = CategoryAssestList;
            }
        }

        void AssignValues(Category _Item)
        {
            EntryCategoryName.Text = _Item.CategoryName;
            SwitchActive.IsToggled = _Item.IsActive.ToLower() == "true" ? true : false;
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            if (EntryCategoryName.Text.Trim() != "")
            {
                Category _CategoryItem = new Category
                {
                    CategoryName = EntryCategoryName.Text,
                    IsActive = SwitchActive.IsToggled.ToString()
                };

                if (_EditItem == null)
                {
                    _CategoryItem.CreatedBy = SessionData.LoginUserName;
                    _CategoryItem.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _categoryLogic.InsertCategoryItem(_CategoryItem);

                    ClearValues();

                    var _Alert = await DisplayAlert("Success", "Category added successfully.\nDo you want to add another Category?", "Yes", "No");
                    if (_Alert)
                    {
                        ClearValues();
                        EntryCategoryName.Focus();
                    }
                    else
                    {
                        LoadList();
                        ChangeLayout(true);
                    }
                }
                else
                {
                    _CategoryItem.UniqueID = _EditItem.UniqueID;
                    _CategoryItem.ModifiedBy = SessionData.LoginUserName;
                    _CategoryItem.ModifiedDate = DateTime.Now.ToString("dd/MM/yyyy");

                    _categoryLogic.UpdateCategoryItem(_CategoryItem);

                    ClearValues();

                    var _Alert = await DisplayAlert("Success", "Category updated successfully.\nDo you want to add another Category?", "Yes", "No");
                    if (_Alert)
                    {
                        _EditItem = null;
                        ClearValues();
                        EntryCategoryName.Focus();
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
                await DisplayAlert("Error", "Category missing", "Ok");
            }
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            ChangeLayout(true);
        }

        void ChangeLayout(bool _Value)
        {
            ListContent.IsVisible = _Value;
            FormContent.IsVisible = !_Value;

            if (_Value)
            {
                ToolbarItems.Add(new ToolbarItem("Add", "Add.png", () =>
                {
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
            EntryCategoryName.Text = "";
            SwitchActive.IsToggled = true;
        }
    }
}