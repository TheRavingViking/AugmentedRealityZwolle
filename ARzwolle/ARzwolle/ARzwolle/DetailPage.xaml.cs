﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ARzwolle.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Webkit;

#if __ANDROID__
using Android.Support.V7.App;
#endif

namespace ARzwolle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        int detailId;
        string url;

		public DetailPage ()
		{
			InitializeComponent ();
        }

        //fills all the fields with data from json object
        public DetailPage(int detailId)
        {
            InitializeComponent();


            this.detailId = detailId;

            Json json = new Json();
            List<Detailinformation> dList = json.GetDetailList<DetailPage>();

            var detailItem = (from d in dList
                             where d.Id == detailId
                              select d);
            foreach (Detailinformation item in detailItem)
            {
                TextTitle.Text = item.Title;
                DetailText.Text = item.Text;
                url = item.LinkReadMore;
                Title = item.Title;
                ReadMoreTitle.Text = item.ReadMore;

                //checks if the field is full, if not it wont show in the application.
                if (item.LinkReadMore == "")
                {
                    ReadMoreButton.IsVisible = false;
                    ReadMoreTitle.IsVisible = false;
                }

                if (item.Img != "")
                {
                    image.IsVisible = true;
                    WebView1.IsVisible = false;
                    image.Source = item.Img;
                }
                else{
                    image.IsVisible = false;
                    WebView1.IsVisible = true;
                }
            }
            //image.IsVisible = true;
        }

        // button click opens website
        void Handle_Clicked(object sender, System.EventArgs e)
		{
            Device.OpenUri(new Uri(url));
		}

        // sets the scanned value to false to start scanning agains
		public void setValueScannedFalse()
		{
			Application.Current.Properties["scanned"] = false;
		}

        // on button click goes back to scanner
		public void MenuItem_OnClicked(object sender, EventArgs e)
	    {
            setValueScannedFalse();
	        this.Navigation.PopModalAsync();
	    }

        // on hardware button click goes back to scanner
        protected override bool OnBackButtonPressed()
        {
            setValueScannedFalse();
            return base.OnBackButtonPressed();
        }


	}
}
