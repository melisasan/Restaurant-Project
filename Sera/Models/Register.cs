using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adınız")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyadınız")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Username{ get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Geçersiz e-mail adresi girdiniz.")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [Required]
         [Compare("Password",ErrorMessage ="Şifreler aynı değil!")]
        [DisplayName("Şifre tekrar")]
        public string Repassword { get; set; }

    }
}