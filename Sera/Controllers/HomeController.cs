using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sera.Entity;

namespace Sera.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public PartialViewResult _OneCikanList()
        {
            return PartialView(db.Uruns.Where(i => i.OneCikan && i.SatistaMi).ToList());
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult Search(string q)
        {
            var p = db.Uruns.Where(i => i.SatistaMi == true);
            if(!string.IsNullOrEmpty(q))
            {
                p = p.Where(i => i.Ad.Contains(q) || i.Aciklama.Contains(q));

            }
            return View(p.ToList());
        }
            
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Uruns.Where(i=>i.SatistaMi).ToList());
        }
        public ActionResult UrunDetay(int id)
        {
            return View(db.Uruns.Where(i=>i.Id==id).FirstOrDefault());
        }
        public ActionResult Urun()
        {
            return View(db.Uruns.ToList());
        }
        public ActionResult UrunList(int id)
        {
            return View(db.Uruns.Where(i=>i.KategoriId==id).ToList());
        }
    }
}