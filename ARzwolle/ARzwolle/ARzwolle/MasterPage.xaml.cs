using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using System.Diagnostics;

#if __ANDROID__
//using ARzwolle.Droid.Model;
using Android.Content;
using ARzwolle.Droid;
#endif

namespace ARzwolle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        NavigationPage Introduction = new NavigationPage(new Introduction());
        NavigationPage GoogleMaps = new NavigationPage(new GoogleMaps());

        public MasterPage()
        {
            InitializeComponent();
        }

        private void IntroductionButton_Clicked(object sender, EventArgs e)
        {
            // Changes the current page to Introduction and stops displaying the menu
            Detail = Introduction;
            IsPresented = false;
        }

        private void GoogleMapsButton_Clicked(object sender, EventArgs e)
        {
            // Changes the current page to GoogleMaps and stops displaying the menu
            Detail = GoogleMaps;
            IsPresented = false;
        }

        private void QRcodeScannerButton_Clicked(object sender, EventArgs e)
        {
            // Creates a ScannerPage, changes the current page to ScannerPage and stops displaying the menu
            NavigationPage QRcodeScanner = new NavigationPage(new ScannerPage());
            Detail = QRcodeScanner;
            IsPresented = false;

        }

        // Disables the back button on the used device
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
