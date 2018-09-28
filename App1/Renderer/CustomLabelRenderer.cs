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
using SalesApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace SalesApp.Droid.Renderer
{
   public class CustomLabelRenderer :  LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var label = (TextView)Control;
            Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");
            label.Typeface = font;
            
        }
    }
}