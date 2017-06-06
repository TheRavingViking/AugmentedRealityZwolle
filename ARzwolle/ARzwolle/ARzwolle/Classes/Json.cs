using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ARzwolle.Classes
{
     class Json
    {
        #if __IOS__
            string resourcePrefix = "ARzwolle.iOS.Assets.JSON.";
        #endif

        #if __ANDROID__
            string resourcePrefix = "ARzwolle.Droid.Assets.JSON.";
        #endif

        List<QRcode> qrCodeList = new List<QRcode>();
        List<Detailinformation> detailList = new List<Detailinformation>();

        public List<QRcode> GetQRList<T>()
        {

            Assembly assembly = typeof(T).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "data.json");

            QRcode[] array;
            using (StreamReader sr = new StreamReader(stream))
            {

                string data = sr.ReadToEnd();
                QRcodeObject qrData = JsonConvert.DeserializeObject<QRcodeObject>(data);

                array = qrData.qrCode;
                foreach (QRcode qrCode in array)
                {
                    qrCodeList.Add(qrCode);
                }

                return qrCodeList;  
            }
        }

        public List<Detailinformation> GetDetailList<T>()
        {
			Assembly assembly = typeof(T).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "details.json");

            Detailinformation[] detailsArray;
            using(StreamReader sr = new StreamReader(stream))
            {
                string data = sr.ReadToEnd();
                DetailObject detailData = JsonConvert.DeserializeObject<DetailObject>(data);

                detailsArray = detailData.DetailInformation;
                foreach (Detailinformation detail in detailsArray)
                {
                    detailList.Add(detail);
                }

            }
            return detailList;
        }

        public Gpslocation[] GetGpsLocation()
        {
            Assembly assembly = typeof(GoogleMaps).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "locations.json");

            Gpslocation[] gpsCollection;

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                GpsCollection gpsColl = JsonConvert.DeserializeObject<GpsCollection>(json);
                gpsCollection = gpsColl.GpsLocations;
            }

            return gpsCollection;
        }

    }
}
