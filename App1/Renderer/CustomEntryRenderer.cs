using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using SalesApp;
using SalesApp.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryFontRenderer))]
[assembly: ExportRenderer(typeof(FormEntry), typeof(CustomEntryRenderer))]
[assembly: ExportRenderer(typeof(FormLargeEntry), typeof(CustomLargeEntryRenderer))]

namespace SalesApp.Droid.Renderer
{
    public class EntryFontRenderer : EntryRenderer
    {
        public EntryFontRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");
                var nativeEditText = (global::Android.Widget.EditText)Control;
                nativeEditText.Typeface = font;
            }
        }
    }

    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");

                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.FromHex("#c2c2c2").ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                nativeEditText.Background = shape;
                nativeEditText.SetHeight(75);
                nativeEditText.SetPadding(15, 15, 15, 15);
                nativeEditText.SetTextColor(Android.Graphics.Color.Black);

                nativeEditText.Typeface = font;
            }
        }
    }

    public class CustomLargeEntryRenderer : EntryRenderer
    {
        public CustomLargeEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Typeface font = Typeface.CreateFromAsset(Context.Assets, "ITCItalic.otf");

                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.FromHex("#c2c2c2").ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                nativeEditText.Background = shape;
                nativeEditText.SetHeight(150);
                nativeEditText.SetPadding(15, 15, 15, 15);

                nativeEditText.Typeface = font;
            }
        }
    }

}