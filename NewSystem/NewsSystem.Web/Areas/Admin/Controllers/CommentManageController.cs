using NewsSystem.Application.Comments;
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
    public class CommentManageController : Controller
    {
        User _user = System.Web.HttpContext.Current.Session["Admin"] as User;
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly CommentAppService _commtAppservce = new CommentAppService();
       // GET: Admin/CommentManage
       public ActionResult GetAllComment()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<CommentViewModel> model = (from m in _ctx.Comment
                             where m.IsDelete == false
                             select new CommentViewModel
                             {

                                 Id = m.Id,
                                 CommentPerson = m.CommentPerson,
                                 CommentTime = m.CommentTime,
                                 CommentContent = m.CommentContent,
                                 NewsId = m.NewsId,
                                 ParentId = m.ParentId
                             }).OrderByDescending(x => x.CommentTime).ToList();
                foreach (var item in model)
                {
                    item.NewsName = GetNewsName(item.NewsId);
                }
                return View(model);
            }
        }
        public ActionResult GetUnPublicComment()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<CommentViewModel> model = (from m in _ctx.Comment
                             where m.IsDelete == false && m.IsPublic ==false
                             select new CommentViewModel
                             {

                                 Id = m.Id,
                                 CommentPerson = m.CommentPerson,
                                 CommentTime = m.CommentTime,
                                 CommentContent = m.CommentContent,
                                 NewsId = m.NewsId,
                                 ParentId = m.ParentId
                             }).OrderByDescending(x => x.CommentTime).ToList();
                foreach (var item in model)
                {
                    item.NewsName = GetNewsName(item.NewsId);
                }
               return View(model);
            }
        }
        public ActionResult GetPublicComment()
        {
            if (_user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<CommentViewModel> model = (from m in _ctx.Comment
                             where m.IsDelete == false && m.IsPublic == true
                             select new CommentViewModel
                             {
                                 Id = m.Id,
                                 CommentPerson = m.CommentPerson,
                                 CommentTime = m.CommentTime,
                                 CommentContent = m.CommentContent,
                                 NewsId = m.NewsId,
                                 ParentId = m.ParentId
                             }).OrderByDescending(x => x.CommentTime).ToList();
                foreach (var item in model)
                {
                    item.NewsName = GetNewsName(item.NewsId);
                }
               return View(model);
            }
        }

        public ActionResult GetAComment(int id)
        {
            List<CommentViewModel> model = (from m in _ctx.Comment
                         where m.Id==id
                         select new CommentViewModel
                         {
                             Id = m.Id,
                             CommentPerson = m.CommentPerson,
                             CommentTime = m.CommentTime,
                             CommentContent = m.CommentContent,
                             NewsId = m.NewsId,
                             ParentId = m.ParentId
                         }).ToList();
            foreach(var item in model)
            {
                item.NewsName = GetNewsName(item.NewsId);
            }
            return View(model);
        }

        public string GetNewsName(int id)
        {
            return _ctx.News.SingleOrDefault(m => m.Id == id).Title;
        }
        public ActionResult PublicComment(int id)
        {
            Comment com = _commtAppservce.PublicComment(id);
            return RedirectToAction("GetPublicComment","CommentManage");
        }

        public ActionResult BackOutComment(int id)
        {
            Comment com = _commtAppservce.BackOutComment(id);
            return RedirectToAction("GetUnPublicComment", "CommentManage");
        }
        public ActionResult DeleteComment(int id)
        {
            Comment com = _commtAppservce.DeleteComment(id);
            return View();
        }
    }
}