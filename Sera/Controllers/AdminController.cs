using Sera.Entity;
using Sera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Sera.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DataContext db = new DataContext();
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            DurumModel model = new DurumModel();
            model.BekleyenSiparisSayisi = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Bekleniyor).ToList().Count();
            model.TamamlananSiparisSayisi = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Tamamlandi).ToList().Count();
            model.YoldaSiparisSayisi = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Yolda).ToList().Count();
            model.TeslimedilenSiparisSayisi = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.TeslimEdildi).ToList().Count();
            model.UrunSayisi = db.Uruns.Count();
            model.SiparisSayisi = db.Orders.Count();
            return View(model);
        }
        public PartialViewResult BildirimMenu()
        {
            var bildirim = db.Orders.Where(i => i.SiparisDurumu == SiparisDurumu.Bekleniyor).ToList();
            return PartialView(bildirim);
        }
    }
}