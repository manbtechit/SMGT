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

        public ReportDataPage(string _ReportName)
        {
            InitializeComponent();

            ListReportValue.IsPullToRefreshEnabled = false;
            ListReportValue.IsRefreshing = IsBusy;

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

            if (_ReportName == "TodayTransaction")
                TodayTransaction();
            if (_ReportName == "CurrentStock")
                CurrentStock();
            if (_ReportName == "Purchase")
                PurchaseReport();
            if (_ReportName == "Sales")
                SalesReport();
        }

        void TodayTransaction()
        {
            this.Title = "Today Transaction";
            LabelTitle.Text = "Today Transaction Report";

            chart1.IsVisible = false;
            ListReportStack.IsVisible = true;
            ListStockStack.IsVisible = false;

            List<SalesOrder_Product> _TodayTrans = new List<SalesOrder_Product>();

            var _ReceiptTrans = _reportLogic.GetTodayReceiptTransaction();
            if(_ReceiptTrans!=null && _ReceiptTrans.Count!=0)
            {
                foreach(var _itm in _ReceiptTrans)
                {
                    _TodayTrans.Add(new SalesOrder_Product() {
                        OrderNumber = _itm.OrderNumber,
                        ProductName = _itm.ProductName,
                        Quantity=_itm.Quantity
                    });
                }
            }

            var _SaleTrans = _reportLogic.GetTodaySalesTransaction();
            if (_SaleTrans != null && _SaleTrans.Count != 0)
            {
                foreach (var _itm in _SaleTrans)
                {
                    _TodayTrans.Add(new SalesOrder_Product()
                    {
                        OrderNumber = _itm.OrderNumber,
                        ProductName = _itm.ProductName,
                        Quantity = _itm.Quantity
                    });
                }
            }

            ListReport.ItemsSource = _TodayTrans;
        }

        void CurrentStock()
        {
            this.Title = "Current Stock";
            LabelTitle.Text = "Current Stock Status";

            chart1.IsVisible = false;
            ListReportStack.IsVisible = false;
            ListStockStack.IsVisible = true;

            List<StockReport> _StockItem = new List<StockReport>();

            var _Stock = _reportLogic.GetAllStockItems();
            if(_Stock!=null && _Stock.Count!=0)
            {
                foreach(var _itm in _Stock)
                {
                    _StockItem.Add(new StockReport() {
                        ProductName=_itm.ProductName,
                        Category=_itm.Category,
                        Quantity=_itm.Quantity.ToString()+" " +_itm.Unit,
                        PurchasePrice = SessionData.Currency + " "+ _itm.PurchasePrice,
                        SalesPrice =SessionData.Currency+" "+ _itm.SalesPrice
                    });
                }

                ListStockReport.ItemsSource = _StockItem;
            }
        }

        void PurchaseReport()
        {
            this.Title = "Purchase Order Report";
            LabelTitle.Text = "Last 10 days Purchase Order Report";

            chart1.IsVisible = true;
            ListReportStack.IsVisible = false;
            ListStockStack.IsVisible = false;

            List<Entry> _Value = new List<Entry>();
            List<ReportModel> _ListValue = new List<ReportModel>();

            var _POList = _reportLogic.GetAllPurchaseOrder();
            if (_POList != null && _POList.Count != 0)
            {
                int index = 0;
                foreach (var _itm in _POList)
                {
                    _Value.Add(new Entry(Convert.ToInt32(_itm.Total))
                    {
                        Color = SKColor.Parse(ColorCodes[index]),
                        Label = _itm.OrderDate,
                        ValueLabel = _itm.Total.ToString()
                    });
                    _ListValue.Add(new ReportModel()
                    {
                        Color = ColorCodes[index],
                        Title = _itm.OrderDate,
                        Value = SessionData.Currency + " " + _itm.Total.ToString()
                    });
                    index += 1;
                }
            }

            IEnumerable<Entry> GetChartData()
            {
                List<Entry> ChartData = _Value;
                return ChartData;
            }

            var chart = new DonutChart() { Entries = GetChartData() };
            this.chart1.Chart = chart;


            ListReportValue.ItemsSource = _ListValue;

            //Chart Types:
            //var chart = new BarChart() { Entries = entries };
            // or: var chart = new PointChart() { Entries = entries };
            // or: var chart = new LineChart() { Entries = entries };
            // or: var chart = new DonutChart() { Entries = entries }; (Pie)
            // or: var chart = new RadialGaugeChart() { Entries = entries };
            // or: var chart = new RadarChart() { Entries = entries };
        }

        void SalesReport()
        {
            this.Title = "Sales Order Report";
            LabelTitle.Text = "Last 10 days Sales Order Report";

            chart1.IsVisible = true;
            ListReportStack.IsVisible = false;
            ListStockStack.IsVisible = false;

            List<Entry> _Value = new List<Entry>();
            List<ReportModel> _ListValue = new List<ReportModel>();

            var _POList = _reportLogic.GetAllSalesOrder();
            if (_POList != null && _POList.Count != 0)
            {
                int index = 0;
                foreach (var _itm in _POList)
                {
                    _Value.Add(new Entry(Convert.ToInt32(_itm.Total))
                    {
                        Color = SKColor.Parse(ColorCodes[index]),
                        Label = _itm.OrderDate,
                        ValueLabel = _itm.Total.ToString()
                    });
                    _ListValue.Add(new ReportModel()
                    {
                        Color = ColorCodes[index],
                        Title = _itm.OrderDate,
                        Value = SessionData.Currency + " " + _itm.Total.ToString()
                    });
                    index += 1;
                }
            }

            IEnumerable<Entry> GetChartData()
            {
                List<Entry> ChartData = _Value;
                return ChartData;
            }

            var chart = new PointChart() { Entries = GetChartData() };
            this.chart1.Chart = chart;


            ListReportValue.ItemsSource = _ListValue;

            //Chart Types:
            //var chart = new BarChart() { Entries = entries };
            // or: var chart = new PointChart() { Entries = entries };
            // or: var chart = new LineChart() { Entries = entries };
            // or: var chart = new DonutChart() { Entries = entries }; (Pie)
            // or: var chart = new RadialGaugeChart() { Entries = entries };
            // or: var chart = new RadarChart() { Entries = entries };
        }

    }

    public class ReportModel
    {
        public string Color { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }

}