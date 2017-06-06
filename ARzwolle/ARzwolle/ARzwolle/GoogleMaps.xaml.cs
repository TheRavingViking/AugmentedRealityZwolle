using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Reflection;
using ARzwolle.Classes;


#if __ANDROID__
using Android.Content;
#endif

namespace ARzwolle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class GoogleMaps : ContentPage
    {
        public List<Pin> GpsCoords = new List<Pin>();
        
        public GoogleMaps()
        {
            InitializeComponent();

            // Puts the default location on Stadshagen, this should be dynamic based on users location. 
            QRcodesMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                new Position(52.5363069, 6.0527966), Distance.FromMiles(1)));

            // Makes new JSON object from json class and sets array with gps locations from json file
            Json jsonObject = new Json();
            Gpslocation[] GpsCollection = jsonObject.GetGpsLocation();

            // Puts every gpslocation in array onto the map. 
            foreach (Gpslocation gpslocation in GpsCollection)
            {
                Pin gps = new Pin
                {
                    Position = new Position(gpslocation.Lat, gpslocation.Long),
                    Label = gpslocation.Desc,
                    Address = gpslocation.Title,
                };

                QRcodesMap.Pins.Add(gps);
            }
        }
    }
}
