using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace SalesApp.Pages
{
    public class HomePage : ContentPage
    {
        ZXingScannerPage scanPage;
        Button buttonScanDefaultOverlay;
        Button buttonScanCustomOverlay;
        Button buttonScanContinuously;
        Button buttonScanCustomPage;
        Button buttonGenerateBarcode;

        public HomePage() : base()
        {
            buttonScanCustomOverlay = new Button
            {
                Text = "Scan with Custom Overlay",
                AutomationId = "scanWithCustomOverlay",
            };
            buttonScanCustomOverlay.Clicked += async delegate {
                // Create our custom overlay
                var customOverlay = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                var torch = new Button
                {
                    Text = "Toggle Torch"
                };
                torch.Clicked += delegate {
                    scanPage.ToggleTorch();
                };
                customOverlay.Children.Add(torch);

                scanPage = new ZXingScannerPage(new ZXing.Mobile.MobileBarcodeScanningOptions { AutoRotate = true }, customOverlay: customOverlay);
                scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync();
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            };

            buttonScanCustomPage = new Button
            {
                Text = "Scan with Custom Page",
                AutomationId = "scanWithCustomPage",
            };
            buttonScanCustomPage.Clicked += async delegate {
                var customScanPage = new CustomScanPage();
                await Navigation.PushModalAsync(customScanPage);
            };

            var stack = new StackLayout();
            stack.Children.Add(buttonScanCustomOverlay);
            stack.Children.Add(buttonScanCustomPage);            

            Content = stack;
        }
    }
}