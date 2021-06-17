using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Entity
{
    public class Urun
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public double Fiyat { get; set; }
        public bool AnasayfadaMi { get; set; }
        public bool OneCikan { get; set; }
        public bool SatistaMi { get; set; }
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }


    }
}