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
	public partial class ReportDataPage : ContentPage
	{
        ReportLogic _reportLogic = new ReportLogic();
        List<string> ColorCodes;

        public ReportDataPage (string _ReportName)
		{
			InitializeComponent ();

            ColorCodes = new List<string>();
            ColorCodes.Add("#FF615B");
            ColorCodes.Add("#FF903B");
            ColorCodes.Add("#FFCE00");
            ColorCodes.Add("#FFF278");
            ColorCodes.Add("#B7E800");
            ColorCodes.Add("#45D65D");
            ColorCodes.Add("#00ECDD");
            ColorCodes.Add("#5ABAE6");
            ColorCodes.Add("#AA88DB");
            ColorCodes.Add("#FF1943");
            ColorCodes.Add("#00BFFF");
            ColorCodes.Add("#00CED1");
            ColorCodes.Add("#aa3834");
            ColorCodes.Add("#fcd083");
            ColorCodes.Add("#c9c7c3");

            if (_ReportName=="Purchase")
            {
                PurchaseReport();
                this.Title = "Purchase Order Report";
                LabelTitle.Text = "Last 10 days Purchase Order Report";
            }
		}

        void PurchaseReport()
        {
            List<Entry> _Value = new List<Entry>();

            var _POList = _reportLogic.GetAllPurchaseOrder();
            if(_POList!=null && _POList.Count!=0)
            {
                int index = 0;
                foreach(var _itm in _POList)
                {
                    _Value.Add(new Entry(Convert.ToInt32(_itm.Total))
                    {
                        Color = SKColor.Parse(ColorCodes[index]),
                        Label = _itm.OrderDate,
                        ValueLabel = _itm.Total.ToString()
                    });

                    index += 1;
                }
            }
            
            IEnumerable<Entry> GetChartData()
            {
                List<Entry> ChartData = _Value;
                return ChartData;
            }

            var chart = new LineChart() { Entries = GetChartData() };
            this.chart1.Chart = chart;
        }
	}
}