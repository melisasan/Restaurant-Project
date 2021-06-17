using Sera.Entity;
using Sera.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sera.Controllers
{
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(Getcart());
        }

        //database e kaydetme
        private void SaveOrder(Cart cart,SiparisDetay model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;
            order.SiparisDurumu = SiparisDurumu.Bekleniyor;
            order.Adress = model.Adres;
            order.Sehir = model.Sehir;
            order.Ilce = model.Ilce;
            order.Mahalle = model.Mahalle;
            order.PostaKodu = model.PostaKodu;
            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Adet;
                orderline.Price = item.Adet * item.Urun.Fiyat;
                orderline.UrunId = item.Urun.Id;
                order.OrderLines.Add(orderline);

            }
            db.Orders.Add(order);
            db.SaveChanges();

        }
        public ActionResult Checkout()
        {
            return View(new SiparisDetay());
        }
        [HttpPost]
        public ActionResult Checkout(SiparisDetay model)
        {
            var cart = Getcart();
            if(cart.CartLines.Count==0)
            {
                ModelState.AddModelError("UrunYok", "Sepetinizde ürün bulunmamaktadır.");

            }
            
            if(ModelState.IsValid)
            {
                SaveOrder(cart, model);
                cart.Clear();
                return View("SiparisTamamlandi");
            }
            else
            {
                return View(model);
            }
        }
        public PartialViewResult Summary()
        {
            return PartialView(Getcart());
        }
        public PartialViewResult SummaryUst()
        {
            return PartialView(Getcart());
        }
        public ActionResult RemoveFromCart(int id)
        {
            var urun = db.Uruns.FirstOrDefault(i => i.Id == id);
            if(urun!=null)
            {
                Getcart().DeleteUrun(urun);
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddToCart(int Id)
        {
            var urun = db.Uruns.FirstOrDefault(i => i.Id == Id);
            if (urun!=null)
            {
                Getcart().AddUrun(urun, 1);
            
            }
            return RedirectToAction("Index");
        }
        //kullanıcıya kart oluşturma(sepete ürün eklemesi için)
        public Cart Getcart()
        {
            var cart = (Cart)Session["Cart"];
            if(cart==null)
            { 
                //kart daha önce oluşmamışsa 
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}