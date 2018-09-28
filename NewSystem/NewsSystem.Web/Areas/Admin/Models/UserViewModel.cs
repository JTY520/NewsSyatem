using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string UserAccout { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public string NickName { get; set; }
    }
}