using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Entity
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<Urun>  Uruns { get; set; }

    }
}