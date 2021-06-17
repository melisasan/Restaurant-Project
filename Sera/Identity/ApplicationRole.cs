using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sera.Identity
{
    public class ApplicationRole:IdentityRole
    {
        public string Aciklama { get; set; }
        public ApplicationRole()
        {

        }
        public ApplicationRole(string rolad,string aciklama)
        {
            this.Aciklama = aciklama;
        }
    }
}