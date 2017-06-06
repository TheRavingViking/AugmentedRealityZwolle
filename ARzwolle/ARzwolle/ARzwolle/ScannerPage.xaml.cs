using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode.Internal;
#if __ANDROID__
using Android.Content;
using ARzwolle.Droid;
#endif


namespace ARzwolle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {

        List<string> idList = new List<string>();
        public ZXingScannerPage scanPage;
        Intent intent = new Intent(Forms.Context, typeof(WikitudeActivity));

        public ScannerPage()
        {
            InitializeComponent();
            Image.Source = "img_QRcode.png";
            ScanBarcode();
        }

        public void ScanBarcode()
        {
            ZXingDefaultOverlay overlay;
            Application.Current.Properties["scanned"] = false;

            //Create an overlay on the scannerpage and sets text on the overlay
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Houd je telefoon over de QR code",
                BottomText = "Scannen begint automatisch.",
            };

            //Creates scannerpage with the created overlay
            scanPage = new ZXingScannerPage(customOverlay: overlay);
            scanPage.Title = "Scan QR code";

            // Checks if the scanner has a result and triggers this function
            scanPage.OnScanResult += (result) =>
            {
                
                if ((bool)Application.Current.Properties["scanned"] == false)
                {
                    // Does something with the device
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // Checks if the result contains a tracker id and a detail id
                        if (result.Text.Contains("tracker_id") && result.Text.Contains("detail_id"))
                        {
#if __ANDROID__
                            if (result.Text.Contains('='))
                            {
                                // Gets the text of the result and splits it on the "," character
                                string resultText = result.Text;
                                char split = ',';
                                string[] stringArray = resultText.Split(split);

                                // Splits every string in stringArray on the "=" charachter and adds it to idList
                                foreach (string id in stringArray)
                                {
                                    char splitOnEqual = '=';
                                    string[] SplitArray = id.Split(splitOnEqual);

                                    string idsplit = SplitArray[1];

                                    idList.Add(idsplit);
                                }

                                // Sets the id's in idList as parameter in the intent and clears the idList
                                intent.PutExtra("target_ID", idList[0]);
                                intent.PutExtra("detail_ID", idList[1]);

                                idList.Clear();

                                // Sets the scanned property to true, starts the activity and closes the scannerpage
                                Application.Current.Properties["scanned"] = true;
                                Forms.Context.StartActivity(intent);
                                await Task.Delay(2000);
                                await Navigation.PopToRootAsync();
                            }
                            else
                            {
                                Application.Current.Properties["scanned"] = true;
                                await DisplayAlert("Error", "Bevat geen tracker of detail", "OK");
                                Application.Current.Properties["scanned"] = false;
                            }
#endif
                        }
                        else
                        {
                            Application.Current.Properties["scanned"] = true;
                            await DisplayAlert("Error", "Dit is geen geldige QR Code", "OK");
                            Application.Current.Properties["scanned"] = false;
                        }

                    });
                }
            };
        }

        private void ButtonScan_OnClicked(object sender, EventArgs e)
        {
            // Navigates to the scannerpage
            Navigation.PushAsync(scanPage);
        }

    }
}