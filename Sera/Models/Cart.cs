using Sera.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    
    public class Cart
    {
        private List<CartLine> _cartLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get { return _cartLines; }
        }
        public void AddUrun(Urun urun,int adet)
        {
            //ürünü sepete ekle
            var line = _cartLines.FirstOrDefault(i => i.Urun.Id == urun.Id);
            //eklediğimiz ürün sepette var mı
            if (line==null)
            {
                _cartLines.Add(new CartLine() { Urun = urun, Adet = adet });
            }
            else
            {
                line.Adet += adet;
            }

        }
        public void DeleteUrun(Urun urun)
        {
            _cartLines.RemoveAll(i => i.Urun.Id == urun.Id);
        }
        public double Total()
        {
            return _cartLines.Sum(i => i.Urun.Fiyat * i.Adet);
        }
        public void Clear()
        {
            _cartLines.Clear();
        }
    }
    public class CartLine
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }

    }
}