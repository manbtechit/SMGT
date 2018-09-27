using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalesApp
{
    public class CustomButton : Button
    {
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Button), default(Thickness));

        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }
    }
}
