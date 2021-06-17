using Sera.Entity;
using Sera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sera.Controllers
{
    public class SiparisController : Controller
    {
        DataContext db = new DataContext();
        // GET: Siparis
        public ActionResult Index()
        {
            //onaylı veya onaysız bütün siparişler için
            var orders = db.Orders.Select(i => new AdminSiparis()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                SiparisDurumu = i.SiparisDurumu,
                Total = i.Total,
                Count = i.OrderLines.Count

            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var model = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetail()
            {
                SiparisId = i.Id,
                Siparisnumara = i.OrderNumber,
                Total = i.Total,
                UserName = i.UserName,
                SiparisTarihi = i.OrderDate,
                SiparisDurumu = i.SiparisDurumu,
                Sehir = i.Sehir,
                Adres = i.Sehir,
                Ilce = i.Ilce,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                {
                    UrunId = x.UrunId,
                    Resim = x.Urun.Resim,
                    UrunAdi = x.Urun.Ad,
                    Adet = x.Quantity,
                    Fiyat = x.Price
                }).ToList()
            }).FirstOrDefault();
            return View(model);
        }

        public ActionResult UpdateSiparisDurumu(int SiparisId,SiparisDurumu siparisDurumu)
        {
            var siparis = db.Orders.FirstOrDefault(i => i.Id == SiparisId);
            if(siparis!=null)
            {
                siparis.SiparisDurumu = siparisDurumu;
                db.SaveChanges();
                TempData["mesaj"] = "Bilgiler kaydedildi.";
                return RedirectToAction("Details", new { id = SiparisId });
            }
            return RedirectToAction("Index");
        }
        public ActionResult BekleyenSiparis()
        {
            var model = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Bekleniyor).ToList();
            return View(model);
        }
        public ActionResult TamamlananSiparis()
        {
            var model = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Tamamlandi).ToList();
            return View(model);
        }
        public ActionResult YoldaSiparis()
        {

            var model = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Yolda).ToList();
            return View(model);
        }
        public ActionResult TeslimedilenSiparis()
        {
            var model = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.TeslimEdildi).ToList();
            return View(model);

        }
    }
}