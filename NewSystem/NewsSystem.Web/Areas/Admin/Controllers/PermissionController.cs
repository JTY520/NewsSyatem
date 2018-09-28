
using NewsSystem.Web.Areas.Admin.Models;
using NewsSystem.Web.Areas.Admin.Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Areas.Admin.Controllers
{
    public class PermissionController : Controller
    {
        private static readonly PermissionManager _permission = new PermissionManager();
        // GET: Admin/Permission
        [HttpGet]
        public ActionResult SetPermissions()
        {
            PermissionViewModel model = new PermissionViewModel();
            var permissions = _permission.GetAllPermissions().ToList();
            permissions.ForEach(p => model.Permissions.Add(new SelectListItem() { Text = p.DisplayName, Value = p.Code }));
            return View(model);
        }

        [HttpPost]
        public ActionResult SetPermissions([Bind(Include = "RequiredAuthorizeCode")]PermissionViewModel model)
        {

            //数据库的相关操作
            return RedirectToAction("Index");
        }
    }
}