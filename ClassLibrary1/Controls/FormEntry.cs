using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SalesApp
{
    public class FormEntry : Entry
    {
        public FormEntry()
        {
            TextChanged += _EnforceMaxLength;
        }

        public int? MaxLength { get; set; }

        private void _EnforceMaxLength(object sender, TextChangedEventArgs args)
        {
            if (!MaxLength.HasValue) return;

            var e = sender as Entry;
            if (e == null)
                return;

            var val = e.Text;

            if (!(val.Length > MaxLength)) return;

            val = val.Remove(val.Length - 1);
            e.Text = val;
        }
    }

    public class FormLargeEntry : Entry
    {
        public FormLargeEntry()
        {
            TextChanged += _EnforceMaxLength;
        }

        public int? MaxLength { get; set; }

        private void _EnforceMaxLength(object sender, TextChangedEventArgs args)
        {
            if (!MaxLength.HasValue) return;

            var e = sender as Entry;
            if (e == null)
                return;

            var val = e.Text;

            if (!(val.Length > MaxLength)) return;

            val = val.Remove(val.Length - 1);
            e.Text = val;
        }
    }
}
