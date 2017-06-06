﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wikitude.Architect;

using Wikitude.Tools.Device.Features;
using Wikitude.Common.Camera;
using Android.Content.PM;
using Org.Json;
using Android.Util;
using ARzwolle.Droid.Resources.Utils;
using Xamarin.Forms;

namespace ARzwolle.Droid
{
    [Activity(Label = "WikitudeActivity", Icon = "@drawable/logoZwolle", Theme = "@style/Wikitude", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class WikitudeActivity : Activity, IArchitectJavaScriptInterfaceListener
    {

        // variables
        ArchitectView architectView;
        // Define the buttons 
        ImageButton detailButton;
        ImageButton HomeButton;
        string worldUrl = "";
        int detailID = 0;
       



        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cam);
  

            Title = "Scan the Image";



            // get the tracker id from the (Previous) scannerpage
            int result = int.Parse(Intent.GetStringExtra("target_ID"));
            // get the detail id from the (Previous) scannerPage
            detailID = int.Parse(Intent.GetStringExtra("detail_ID"));
           
            // Checks the tracker id. If its 1 - 4 it will like to the corresponding folder. Else if will load first AR object
            switch (result)
            {
                case 1:
                    worldUrl = "Augmented/Barrel/index.html";
                    break;
                case 2:
                    worldUrl = "Augmented/Tree/index.html";
                    break;
                case 3:
                    worldUrl = "Augmented/FireRedTree/index.html";
                    break;
                case 4:
                    worldUrl = "Augmented/secondWoodenBarrel/index.html";
                    break;

                default:
                    worldUrl = "Augmented/Barrel/index.html";
                    break;
            }

            // finds the view on the resource page and stores it in the corresponding variables.
            architectView = FindViewById<ArchitectView>(Resource.Id.architectView);
            detailButton = FindViewById<ImageButton>(Resource.Id.detailButton);
            HomeButton = FindViewById<ImageButton>(Resource.Id.HomeButton);

            // adds a button click handler to the buttons
            detailButton.Click += Detail_HandleClick;
            HomeButton.Click += Home_HandleClick;


            // setting data for the architectview
            ArchitectStartupConfiguration startup = new ArchitectStartupConfiguration();
            // set the licensekey
            startup.setLicenseKey(TotallyNotAKeyClass.WIKITUDE_KEY);
            startup.setFeatures(ArchitectStartupConfiguration.Features.ImageTracking);
            startup.setCameraResolution(CameraSettings.CameraResolution.Auto);

            // features that are required
            int requiredFeatures = ArchitectStartupConfiguration.Features.ImageTracking;
            MissingDeviceFeatures missingDeviceFeatures = ArchitectView.isDeviceSupported(this, requiredFeatures);


            //checks if the required features defined above are located on the android device
            if ((ArchitectView.getSupportedFeaturesForDevice(Android.App.Application.Context) & requiredFeatures) == requiredFeatures)
            {
                architectView.OnCreate(startup);
                architectView.AddArchitectJavaScriptInterfaceListener(this);
            }
            else
            {
                architectView = null;
                Toast.MakeText(this, missingDeviceFeatures.getMissingFeatureMessage(), ToastLength.Long).Show();

                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetMessage("Something went wrong");
                //StartActivity(typeof(ErrorActivity));
            }

            // if the detailbutton is pressed it will redirect to the details.xaml page

        }

        public void OnJSONObjectReceived(JSONObject p0)
        {
            throw new NotImplementedException();
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (architectView != null)
                architectView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (architectView != null)
                architectView.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (architectView != null)
            {
                architectView.OnDestroy();
            }
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();

            if (architectView != null)
                architectView.OnLowMemory();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            if (architectView != null)
            {
                architectView.OnPostCreate();

                try
                {
                    architectView.Load(worldUrl);
                }
                catch (Exception ex)
                {
                    Log.Error("WIKITUDE_SAMPLE", ex.ToString());
                }
            }
        }

        private void Detail_HandleClick(object sender, EventArgs e)
        {
            OpenDetailPage();
        }

		// if the Home icon is clicked the scanninng will be disabled
		private void Home_HandleClick(object sender, EventArgs e)
        {
            
            App.Current.Properties["scanned"] = false;
            this.Finish();
        }

        // redirects to the detailpage with the corresponding detail id
        public void OpenDetailPage()
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DetailPage(detailID)));
			this.Finish();
        }

		public override void OnBackPressed()
        {
            
        }
		
    }
}