using System;
using System.Collections.Generic;
using System.Text;

namespace ARzwolle.Classes
{
    public class QRcode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tracker { get; set; }
    }

    public class QRcodeObject
    {
        public QRcode[] qrCode { get; set; }
    }
}


