using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    public class DurumModel
    {
        public int UrunSayisi { get; set; }
        public int SiparisSayisi { get; set; }
        public int BekleyenSiparisSayisi { get; set; }
        public int TamamlananSiparisSayisi { get; set; }
        public int YoldaSiparisSayisi { get; set; }
        public int TeslimedilenSiparisSayisi { get; set; }
    }
}