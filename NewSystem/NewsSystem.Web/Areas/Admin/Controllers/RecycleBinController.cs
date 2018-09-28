using NewsSystem.Application.NewsNews;
using NewsSystem.Application.Users;
using NewsSystem.Core.RecycleBin;
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
    public class RecycleBinController : Controller
    {
        // GET: Admin/RecycleBin
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly RecycleBinManage _recycleBinManage = new RecycleBinManage();
        User _user = System.Web.HttpContext.Current.Session["Admin"] as User;

        public ActionResult GetRecycleNews()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<NewsViewModel> model = (from m in _ctx.News
                             where m.IsDelet == true
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

        public ActionResult GetRecycleComment()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
              List<CommentViewModel>model = (from m in _ctx.Comment
                             where m.IsDelete == true
                             select new CommentViewModel
                             {
                                 Id = m.Id,
                                 CommentPerson = m.CommentPerson,
                                 CommentTime = m.CommentTime,
                                 CommentContent = m.CommentContent,
                                 NewsId = m.NewsId,
                                 ParentId = m.ParentId
                             }).OrderByDescending(x => x.CommentTime).ToList();
                return View(model);
            }
        }
        public ActionResult GetRecycleUser()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
               List<UserViewModel> model = (from m in _ctx.User
                             where m.IsDelet == true
                             select new UserViewModel
                             {
                                UserAccout = m.UserAccout,
                                Password = m.Password
                             }).ToList();
                return View(model);
            }
        }

        public ActionResult NewsRecover(int id)
        {
            News news = _recycleBinManage.NewsRecover(id);
            return RedirectToAction("GetUnPublicNews", "NewsManage");
        }

        public ActionResult UserRecover(string account)
        {
            User user = _recycleBinManage.UserRecover(account);
            return RedirectToAction("GetAllUsers", "UserManage");
        }
        public ActionResult CommentRecover(int id)
        {
            Comment com = _recycleBinManage.CommentRecover(id);
            return RedirectToAction("GetUnPublicComment", "CommentManage");
        }

   
    }
}