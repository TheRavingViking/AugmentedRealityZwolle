using System;
namespace ARzwolle
{
    public class GpsCollection
    {
        public Gpslocation[] GpsLocations { get; set; }
    }

    public class Gpslocation
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }

}

