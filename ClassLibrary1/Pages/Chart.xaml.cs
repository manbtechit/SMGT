using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chart : ContentPage
    {
        public Chart()
        {
            InitializeComponent();
            
            List<Entry> _vaue = new List<Entry>();
            _vaue.Add(new Entry(200)
            {
                Color = SKColor.Parse("#FF1943"),
                Label = "January",
                ValueLabel = "200"
            });
            _vaue.Add(new Entry(300)
            {
                Color = SKColor.Parse("#00BFFF"),
                Label = "March",
                ValueLabel = "300"
            });
            _vaue.Add(new Entry(600)
            {
                Color = SKColor.Parse("#00CED1"),
                Label = "Octobar",
                ValueLabel = "600"
            });

            IEnumerable<Entry> GetBooks()
            {
                List<Entry> books = _vaue;
                return books;
            }

            var chart = new LineChart() { Entries = GetBooks() };
            this.chart1.Chart = chart;

        }
    }
}