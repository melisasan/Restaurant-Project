using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sera.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DisplayName("Eski şifre")]
        public string OldPassword { get; set; }
        [Required]
        [DisplayName("Yeni şifre")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Şifreniz en az 6  karakter olmalı!")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Şifre tekrar")]
        [Compare("NewPassword",ErrorMessage ="Şifreler aynı değil!")]
        public string ConNewPassword { get; set; }


    }
}