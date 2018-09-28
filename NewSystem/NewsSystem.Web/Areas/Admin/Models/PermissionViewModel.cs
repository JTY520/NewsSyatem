using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Areas.Admin.Models
{
    public class PermissionViewModel
    {
        public string RequiredAuthorizeCode { set; get; }

        public List<SelectListItem> Permissions { get; set; }

        public PermissionViewModel()
        {
            Permissions = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "无权限", Value = string.Empty, Selected = true }
            };
        }
    }
}