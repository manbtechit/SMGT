using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using Entry = Microcharts.Entry;

namespace SalesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : OrientationContentPage
    {
        ReportLogic _reportLogic = new ReportLogic();
        StockLogic _stockLogic = new StockLogic();
        POLogic _poLogic = new POLogic();
        SalesLogic _saleLogic = new SalesLogic();

        public Dashboard()
        {
            InitializeComponent();
            
            this.ClassId = "Dashbord";
            this.BackgroundColor = Color.White;

            var _Count = _stockLogic.GetAllStockPrice("Count");
            if (_Count != null)
                LabelTotalStock.Text = "Total Stock in hand : " + _Count.FirstOrDefault().Quantity + " of " + _Count.FirstOrDefault().AlertQuantity + " item";

            var _Price = _stockLogic.GetAllStockPrice("Price");
            if (_Price != null)
                LabelTotalStockPrice.Text = "Total Stock Price : " + SessionData.Currency + "" + _Price.FirstOrDefault().PurchasePrice + " / " + SessionData.Currency + "" + _Price.FirstOrDefault().SalesPrice;

            var _Purchase = _poLogic.GetAllPurchaseOrder();
            if(_Purchase!=null)
                LabelOpenPurchase.Text = "Open Purchase Order : "+_Purchase.Count;

            var _Sales = _saleLogic.GetTodaySalesOrder();
            LabelSalesOrder.Text = "Total Sales Order today : "+_Sales.Count;

            LoadClicks();

            LoadSalesReport();

            if(Device.Idiom == TargetIdiom.Tablet)
            {
                OnOrientationChanged += DeviceRotated;
                PageOrientationEventArgs _e = new PageOrientationEventArgs(PageOrientation.Vertical);
                DeviceRotated(null, _e);
            }
            else
            {
                RowChart.Height = 200;
                SalesChart.HeightRequest = 200;
            }
        }

        private void DeviceRotated(object s, PageOrientationEventArgs e)
        {
            switch (e.Orientation)
            {
                case PageOrientation.Horizontal:
                    OverallStack.WidthRequest = Utilities.TabletWidth;
                    SalesChart.WidthRequest = 100;
                    SalesChart.HeightRequest = 250;
                    RowChart.Height = 250;
                    break;
                case PageOrientation.Vertical:
                    OverallStack.WidthRequest = Utilities.TabletWidth;
                    SalesChart.WidthRequest = 100;
                    SalesChart.HeightRequest = 400;
                    RowChart.Height = 400;
                    break;             
            }
        }

        void LoadClicks()
        {
            var StocktapRecognizer = new TapGestureRecognizer();
            StocktapRecognizer.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new StockDetailsPage(null));
            };
            ImageStock.GestureRecognizers.Add(StocktapRecognizer);

            LabelStock.GestureRecognizers.Add(StocktapRecognizer);

            var PurchasetapRecognizer = new TapGestureRecognizer();
            PurchasetapRecognizer.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new PurchaseOrderPage("Add"));
            };
            ImagePurchase.GestureRecognizers.Add(PurchasetapRecognizer);
            LabelPurhase.GestureRecognizers.Add(PurchasetapRecognizer);

            var ReceipttapRecognizer = new TapGestureRecognizer();
            ReceipttapRecognizer.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new ReceiptPage("Add"));
            };
            ImageReceipt.GestureRecognizers.Add(ReceipttapRecognizer);
            LabelReceipt.GestureRecognizers.Add(ReceipttapRecognizer);

            var SaletapRecognizer = new TapGestureRecognizer();
            SaletapRecognizer.Tapped += (sender, eventergs) =>
            {
                Utilities.PushModalAsync(Navigation, new SalesPage("Add"));
            };
            ImageSale.GestureRecognizers.Add(SaletapRecognizer);
            LabelSale.GestureRecognizers.Add(SaletapRecognizer);
        }

        void LoadSalesReport()
        {
            List<string> ColorCodes = new List<string>();
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

            var chart = new DonutChart() { Entries = GetChartData() };
            this.SalesChart.Chart = chart;
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            var _alert = DisplayAlert("Message", "Are you sure want to Logout?", "Yes", "No");

            return true;
        }

    }
}