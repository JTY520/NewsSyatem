using NewsSystem.Application.NewsNews;
using NewsSystem.EntityFramwork;
using NewsSystem.Web.Areas.Admin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Areas.Admin.Controllers
{
    public class NewsManageController : Controller
    {
        // GET: Admin/NewsManage
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly NewsAppService _newsService = new NewsAppService();
        User _user = System.Web.HttpContext.Current.Session["Admin"] as User;

        public ActionResult WriteNews()
        {
            if (_user != null)
                return View();
            else
                return RedirectToAction("Login", "UserManage");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WriteNews(NewsViewModel model)// content, title, firstType, secondType, writer
        {
            try
            {
                News news = _newsService.AddNews(model.Content, model.Title, model.FirstType, model.SecondType, model.Writer);
                return RedirectToAction("GetAllNews", "NewsManage");
            }
            catch 
            {
                return View(); ;
            }
        }


        [HttpGet]
        public ActionResult NewsUpdate(int id)
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.Id == id
                                         select new NewsViewModel
                                         {
                                             Content = m.Content,
                                             FirstType = m.FirstType,
                                             SecondType = m.SecondType,
                                             UpTime = m.UpTime,
                                             Title = m.Title,
                                             Writer = m.Writer
                                         }).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult NewsUpDate(int id, NewsViewModel model)
        {
            try
            {
                News news = _newsService.UpdateNews(id, model.Content, model.Title, model.FirstType, model.SecondType, model.Writer);
                return RedirectToAction("GetAllNews", "NewsManage");
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public ActionResult GetAllNews()
        {
            if (_user == null)
               return  RedirectToAction("Login", "UserManage");
            else
            {
                List<NewsViewModel> model = (from m in _ctx.News
                             where m.IsDelet == false
                             select new NewsViewModel
                             {
                                 Id = m.Id,
                                 Title = m.Title,
                                 Writer = m.Writer,
                                 UpTime = m.UpTime,
                                 FirstType = m.FirstType,
                                 SecondType = m.SecondType,
                                 IsPublish = m.IsPublish
                             }).OrderByDescending(x => x.UpTime).ToList();
                return View(model);
            }
        }

        public ActionResult GetPublicNews()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<NewsViewModel> model = (from m in _ctx.News
                             where m.IsDelet == false && m.IsPublish == true
                             select new NewsViewModel
                             {
                                 Id = m.Id,
                                 Title = m.Title,
                                 Writer = m.Writer,
                                 UpTime = m.UpTime,
                                 FirstType = m.FirstType,
                                 SecondType = m.SecondType,
                             }).OrderByDescending(x => x.UpTime).ToList();
                return View(model);
            }
        }

        public ActionResult GetUnPublicNews()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                 List<NewsViewModel> model = (from m in _ctx.News
                             where m.IsDelet == false && m.IsPublish == false
                             select new NewsViewModel
                             {
                                 Id = m.Id,
                                 Title = m.Title,
                                 Writer = m.Writer,
                                 UpTime = m.UpTime,
                                 FirstType = m.FirstType,
                                 SecondType = m.SecondType,
                             }).OrderByDescending(x => x.UpTime).ToList();
                return View(model);
            }
        }

        public ActionResult NewsDelete(int id)
        {
            News news = _newsService.DeletNews(id);
            return RedirectToAction("GetAllNews", "NewsManage");
        }
        public ActionResult NewsPublic(int id)
        {
            News news = _newsService.NewsPublic(id);
            return RedirectToAction("GetPublicNews","NewsManage");
        }

        public ActionResult NewsBackOut(int id)//撤销
        {
            News news = _newsService.NewsBackOut(id);
            return RedirectToAction("GetUnPublicNews", "NewsManage");
        }

        [HttpGet]
        public ActionResult AddNewsType()
        {
            List<NewsTypeViewModel> model = (from m in _ctx.meun
                                             where m.IsDelete == false && m.ParentId == null
                                             select new NewsTypeViewModel
                                             {
                                                 Id = m.Id,
                                                 Name = m.Name
                                             }).ToList();
            return PartialView("_AddNewsType",model);
        }
        [HttpPost]
        public ActionResult AddNewsType(string typeName,Nullable<int> parentId)
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                try
                {
                    _newsService.AddNewsType(typeName,parentId);
                    return Json("添加成功",JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("添加失败!此项已存在", JsonRequestBehavior.AllowGet);
                }
               
            }
        }

        public ActionResult GetAllNewsType()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<NewsTypeViewModel> model = (from m in _ctx.meun
                                                 where m.IsDelete == false && m.ParentId == null
                                                 select new NewsTypeViewModel
                                                 {
                                                     Id = m.Id,
                                                     Name = m.Name
                                                 }).ToList();
                foreach(var item in model)
                {
                    item.Chlidren = (from m in _ctx.meun
                                     where m.IsDelete == false && m.ParentId == item.Id
                                     select new NewsTypeViewModel
                                     {
                                         Id = m.Id,
                                         Name = m.Name
                                     }).ToList();
                }
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult UpdateNewsType(int id)
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                meun type = _newsService.GetNewsType(id);
                ViewBag.Id = id;
                ViewBag.TypeName = type.Name;
                ViewBag.ParentName = _newsService.GetNewsTypeParentName(type.ParentId);
                ViewBag.ParentId = type.ParentId;

                if (_newsService.NewsTypeExitChildren(id) == true)
                {
                    return View();
                }
                else
                {
                    List<NewsTypeViewModel> model = (from m in _ctx.meun
                                                     where m.IsDelete == false && m.ParentId == null
                                                     select new NewsTypeViewModel
                                                     {
                                                         Id = m.Id,
                                                         Name = m.Name
                                                     }).ToList();
                    return View(model);
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateNewsType(int id,Nullable<int> parentId,string typeName)
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                try
                {
                    meun m = _newsService.UpdateNewsType(id, parentId, typeName);
                    return Json("修改成功", JsonRequestBehavior.AllowGet);
                }

                catch
                {
                    return Json("修改失败", JsonRequestBehavior.AllowGet);
                }
                 
            }
               
        }

        public ActionResult GetOneNews(int id)
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.Id == id
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             Content = m.Content,
                                             FirstType = m.FirstType,
                                             SecondType = m.SecondType,
                                             Writer = m.Writer,
                                             CommentNumber = m.CommentNumber
                                         }).ToList();
            foreach (var m in model)
            {
                m.AllImagesUrl = _newsService.GetAllImgUrl(m.Content);
            }
            return View(model);
        }
        public ActionResult DeleteNewType(int id)
        {
            meun m = _newsService.DeleteNewsType(id);
            return RedirectToAction("GetAllNewsType", "NewsManage");
        }
    }
}