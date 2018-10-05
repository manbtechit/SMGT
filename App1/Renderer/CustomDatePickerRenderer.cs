using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using SalesApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePickerRenderer))]

namespace SalesApp.Droid.Renderer
{
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");

            var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
            shape.Paint.Color = Xamarin.Forms.Color.FromHex("#c2c2c2").ToAndroid();
            shape.Paint.SetStyle(Paint.Style.Stroke);
            Control.SetBackground(shape);
            Control.Typeface = font;
            Control.SetPadding(5, 0, 0, 0);
            Control.SetHeight(30);

            Control.TextSize = 12;
        }
    }
}