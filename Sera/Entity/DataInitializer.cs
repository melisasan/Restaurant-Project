using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Kategori>()
            {
                new Kategori() {Ad="HAMBURGER" },
                new Kategori() {Ad ="PIZZA" },
                new Kategori() { Ad ="SALATALAR" },

            };
            foreach (var kategori in kategoriler)
            {
                context.Kategoris.Add(kategori);

            }
            context.SaveChanges();

            var urunler = new List<Urun>()
            {
                new Urun() {Ad="Karışık Pizza",Aciklama="Sosis,Turşu,Siyah Zeytin,Mısır",Resim="2.jpg",Fiyat=35,AnasayfadaMi=true,OneCikan=true,SatistaMi=true,KategoriId=2 },
                new Urun() {Ad="Hamburger",Aciklama="Et köftesi,Turşu,ketçap,mayonez,marul",Resim="1.jpg",Fiyat=25,AnasayfadaMi=true,OneCikan=true,SatistaMi=true,KategoriId=1 },
                new Urun() {Ad="Sezar Salata",Aciklama="Tavuk,mayonez,sezar sos,mısır ",Resim="3.jpg",Fiyat=25,AnasayfadaMi=true,OneCikan=true,SatistaMi=true,KategoriId=3},
            };
            foreach (var urun in urunler)
            {
                context.Uruns.Add(urun);
            }
            context.SaveChanges();
            base.Seed(context);
        }


    }
}