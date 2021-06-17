using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Adı")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Sifre")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }


    }
}