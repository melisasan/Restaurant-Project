using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Sera.Entity;
using Sera.Identity;
using Sera.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Sera.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        //Sistemde kayıtlı olan kullanıcılara ulaşmak
        public PartialViewResult UserCount()
        {
            var u = UserManager.Users;
            return PartialView(u);
        }
        public ActionResult UserList()
        {
            var u = UserManager.Users;
            return View(u);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            //kullanıcı zorunlu alanları doldurmuş mu
            if (ModelState.IsValid)
            {
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return View("Update");
            }
             return View(model);
        }

        //sayfanın get kısmı
        public ActionResult UserProfil()
        {
            //şuan bağlı olan kullanıcı idsi
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            //oluşturduğumuz modeli kullanma,
            var data = new UserProfile()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName
            };
            return View(data);
        }
        //post kısmı(butona tıklandıktan sonra gerçekleşen olay)
        [HttpPost]
        public ActionResult UserProfil(UserProfile model)
        {
            var user = UserManager.FindById(model.Id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;
            UserManager.Update(user);
            return View("Update");
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Login(Login model,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);
                if(user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var Identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    //remeber me kısmı
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.Rememberme;
                    authManager.SignIn(authProperties, Identityclaims);
                    if(!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);

                    }
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok.");

                }

            }
            return View(model);
        } 
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Username;
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturulurken bir hata oluştu.");
                }
            }
             return View(model);
        
        }
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.Orders.Where(i => i.UserName == username).Select(i => new KullaniciSiparis
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                SiparisDurumu = i.SiparisDurumu,
                OrderDate = i.OrderDate,
                Total = i.Total
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
                SiparisTarihi = i.OrderDate,
                SiparisDurumu = i.SiparisDurumu,
                Adres = i.Adress,
                Sehir = i.Sehir,
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
    }
}