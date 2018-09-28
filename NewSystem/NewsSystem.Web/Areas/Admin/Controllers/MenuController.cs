using NewsSystem.EntityFramwork;
using NewsSystem.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        private readonly MESDbContext _db = new MESDbContext();
        public ActionResult Meun()
        {
            List<MenuItems> model = GreatMeunModel();

            return PartialView("_Meun", model);
        }
        public List<MenuItems> GreatMeunModel()
        {
            List<MenuItems> model = (from m in _db.NewsMenu
                                     where m.ParentId == null
                                     select new MenuItems
                                     {
                                         Id = m.Id,
                                         ParentId = m.ParentId,
                                         Name = m.Name,
                                         Url=m.Url
                                     }).ToList();
            foreach (var item in model)
            {
                item.Children = GetChild(item.Id);
            }

            return model;
        }
        public List<MenuItems> GetChild(int id)
        {

            var kids = _db.NewsMenu.Where(k => k.ParentId == id).ToList();
            List<MenuItems> child = new List<MenuItems>();
            foreach (var items in kids)
            {
                MenuItems kid = new MenuItems();
                {
                    kid.Id = items.Id;
                    kid.ParentId = items.ParentId;
                    kid.Name = items.Name;
                    kid.Url = items.Url;
                    kid.Children =  GetChild(items.Id);
                }
                child.Add(kid);
            }
            return child;
        }
    }
}