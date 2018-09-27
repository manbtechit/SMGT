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


            var entries = new[]
                {
                     new Entry(200)
            {
                Color=SKColor.Parse("#FF1943"),
                Label ="January",
                ValueLabel = "200"
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "March",
                ValueLabel = "400"
            },
            new Entry(100)
            {
                Color =  SKColor.Parse("#00CED1"),
                Label = "Octobar",
                ValueLabel = "100"
            }
                };

            //List<ChartValues> _vaue = new List<ChartValues>();
            //_vaue.Add(new ChartValues()
            //{
            //    Label = "Manoj1",
            //    ValueLabel = "400",
            //    Color = "#00CED1",
            //    TextColor = "#000000"
            //});
            //_vaue.Add(new ChartValues()
            //{
            //    Label = "Manoj2",
            //    ValueLabel = "300",
            //    Color = "#00CED1",
            //    TextColor = "#000000"
            //});
            //_vaue.Add(new ChartValues()
            //{
            //    Label = "Manoj3",
            //    ValueLabel = "200",
            //    Color = "#00CED1",
            //    TextColor = "#000000"
            //});

            //Entry[] entries1 = new Entry[] { _vaue[0] };
            //foreach (var item in _vaue)
            //{
            //    entries1 =new Entry[] {
            //    new Entry(100)
            //    {
            //        Label = item.Label,
            //        ValueLabel = item.ValueLabel,
            //        Color = SKColor.Parse(item.Color),
            //        TextColor = SKColor.Parse(item.TextColor)
            //    }
            //};
            //}

            var chart = new LineChart() { Entries = entries };
            this.chart1.Chart = chart;

        }
    }

    public class ChartValues
    {
        public string Color { get; set; }
        public string Label { get; set; }
        public string ValueLabel { get; set; }
        public string TextColor { get; set; }
    }
}