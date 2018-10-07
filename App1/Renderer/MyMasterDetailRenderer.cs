using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SalesApp.Droid.Renderer;
using Xamarin.Forms.Platform.Android;
using SalesApp;
using Xamarin.Forms;
using Android.Support.V4.Widget;

//[assembly: Xamarin.Forms.ExportRenderer(typeof(MasterDetailPage), typeof(MyMasterDetailRenderer))]

namespace SalesApp.Droid.Renderer
{
    public class MyMasterDetailRenderer : MasterDetailRenderer
    {
        bool firstDone;

        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement)
        {
            base.OnElementChanged(oldElement, newElement);

            var width = Resources.DisplayMetrics.WidthPixels;

            var fieldInfo = GetType().BaseType.GetField("_masterLayout", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var _masterLayout = (ViewGroup)fieldInfo.GetValue(this);
            var lp = new DrawerLayout.LayoutParams(_masterLayout.LayoutParameters);

            MasterDetailPage page = (MasterDetailPage)newElement;
            lp.Width = (int)(100);

            lp.Gravity = (int)GravityFlags.Left;
            _masterLayout.LayoutParameters = lp;
        }

        //public override void AddView(View child)
        //{


        //    //if (firstDone)
        //    //{
        //    //    MyMasterDetailPage page = (MyMasterDetailPage)this.Element;
        //    //    LayoutParams p = (LayoutParams)child.LayoutParameters;
        //    //    p.Width = 300;
        //    //    base.AddView(child, p);
        //    //}
        //    //else
        //    //{
        //    //    firstDone = true;
        //    //    base.AddView(child);
        //    //}
        //}

    }
}