using System;
using System.Collections.Generic;
using System.Text;

namespace ARzwolle.Classes
{


    public class DetailObject
    {
        public Detailinformation[] DetailInformation { get; set; }
    }

    public class Detailinformation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
        public string ReadMore { get; set; }
        public string LinkReadMore { get; set; }
        public int TrackerId { get; set; }
    }

}