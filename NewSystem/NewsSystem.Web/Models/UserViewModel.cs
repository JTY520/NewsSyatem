using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Models
{
    public class UserViewModel
    {
        public string UserAccout { get; set; }
        public string Password { get; set; }

        public Nullable<bool> IsDelet { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string NickName { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public Nullable<System.DateTime> Brithday { get; set; }
        public string Address { get; set; }
        public string Autograph { get; set; }
    }
}