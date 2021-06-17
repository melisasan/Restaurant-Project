using Sera.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    public class KullaniciSiparis
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public SiparisDurumu SiparisDurumu { get; set; }
    }

}