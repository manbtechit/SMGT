
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using SalesApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]

namespace SalesApp.Droid.Renderer
{
    public class CustomNavigationRenderer : NavigationPageRenderer
    {
        public CustomNavigationRenderer(Context context) : base(context)
        {

        }

        public Android.Support.V7.Widget.Toolbar toolbarTop;
        public static string ScreenTitle = "";

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            var navigationPage = (NavigationPage)sender;

            if (Element != null)
            {
                var toolbarTop1 = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_top);

                if (toolbarTop != null)
                {
                    var mTitle = (TextView)toolbarTop1.FindViewById(Resource.Id.toolbar_title);

                    var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "ITCItalic.otf");

                    navigationPage.CurrentPage.Title = "";
                    mTitle.Text = !string.IsNullOrEmpty(navigationPage.CurrentPage.ClassId) ? navigationPage.CurrentPage.ClassId : ScreenTitle;
                    mTitle.Typeface = spaceFont;
                    mTitle.TextSize = 25;
                    mTitle.TextAlignment = Android.Views.TextAlignment.TextStart;
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                toolbarTop = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar_top);

                if (toolbarTop != null)
                {
                    var mTitle = (TextView)toolbarTop.FindViewById(Resource.Id.toolbar_title);

                    var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "ITCItalic.otf");
                    mTitle.Text = e.NewElement.CurrentPage.ClassId;
                    mTitle.Typeface = spaceFont;
                    mTitle.TextSize = 25;
                    mTitle.TextAlignment = Android.Views.TextAlignment.TextStart;
                    ScreenTitle = e.NewElement.CurrentPage.ClassId;
                    toolbarTop.Title = e.NewElement.CurrentPage.ClassId;
                }
            }
        }

        //protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        //{
        //    base.OnElementChanged(e);

        //    RemoveAppIconFromActionBar();
        //}
        //void RemoveAppIconFromActionBar()
        //{
        //    var actionBar = ((Activity)Context).ActionBar;
        //    actionBar.SetIcon(new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()));
        //}

        //private Android.Support.V7.Widget.Toolbar toolbar;

        //public override void OnViewAdded(Android.Views.View child)
        //{
        //    base.OnViewAdded(child);
        //    if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
        //    {
        //        toolbar = (Android.Support.V7.Widget.Toolbar)child;
        //        toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
        //    }
        //}

        //private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        //{
        //    var view = e.Child.GetType();
        //    if (e.Child.GetType() == typeof(Android.Widget.TextView))
        //    {
        //        var textView = (Android.Widget.TextView)e.Child;
        //        var spaceFont = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "ITCItalic.otf");
        //        textView.Typeface = spaceFont;
        //        toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
        //    }
        //}
    }

}