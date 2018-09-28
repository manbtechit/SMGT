using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SalesApp;
using SalesApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomerButtonRenderer))]

namespace SalesApp.Droid.Renderer
{
    public class CustomerButtonRenderer : ButtonRenderer
    {
        public CustomerButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");

            base.OnElementPropertyChanged(sender, e);
            this.Control.SetPadding(
                (int)((CustomButton)this.Element).Padding.Left,
                (int)((CustomButton)this.Element).Padding.Top,
                (int)((CustomButton)this.Element).Padding.Right,
                (int)((CustomButton)this.Element).Padding.Bottom);
            this.Control.Typeface = font;
        }
    }
}