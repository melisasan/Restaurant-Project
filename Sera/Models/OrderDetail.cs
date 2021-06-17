using Sera.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Sera.Models
{
    public class OrderDetail
    {
        public int SiparisId { get; set; }
        public string Siparisnumara { get; set; }
        public double Total { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public SiparisDurumu SiparisDurumu { get; set; }
        public string UserName { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }
        public virtual List<OrderLineModel> OrderLines { get; set; }



    }
    public class OrderLineModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public double Fiyat { get; set; }
        public string Resim { get; set; }


    }

}