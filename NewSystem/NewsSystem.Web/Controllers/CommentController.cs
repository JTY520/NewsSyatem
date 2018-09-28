using NewsSystem.Application.Comments;
using NewsSystem.EntityFramwork;
using NewsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly CommentAppService _comment= new CommentAppService();
        User _user = System.Web.HttpContext.Current.Session["User"] as User;
        public ActionResult GetNewsComment(int id)
        {
            return PartialView("_GetAllComment");
        }
        [HttpGet]
        public ActionResult WriteComment(int commentNumber,int newsId)
        {
            ViewBag.commentNumber = commentNumber;
            ViewBag.newsId = newsId;
            return PartialView("_WriteComment");
        }
        [HttpPost]
        public ActionResult WriteComment(int newsId,string commentContent)
        {
            if (_user != null)
            {
                try
                {
                    Comment com = _comment.WriteComment(newsId, _user, commentContent, null);
                    return Json("评论成功", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("提交失败", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("你还没有登陆,无法评论", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult ReplyComment(int newsId,int parentId,string replyContent)
        {
            if (_user != null)
            {
                try
                {
                    Comment com = _comment.WriteComment(newsId, _user, replyContent, parentId);
                    return Json("回复成功", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("回复失败", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("你还没有登陆，不能回复", JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult GetAllComments(int commentNumber,int newsId)
        {
            ViewBag.commentNumber = commentNumber;
            List<CommentViewModel> model = (from m in _ctx.Comment
                                            where m.ParentId == null&&newsId==m.NewsId&&m.IsDelete==false
                                            select new CommentViewModel
                                            {
                                                Id = m.Id,
                                                ParentId = m.ParentId,
                                                CommentContent = m.CommentContent,
                                                CommentPerson = m.CommentPerson,
                                                CommentTime = m.CommentTime,
                                                NewsId=m.NewsId
                                            }).OrderByDescending(x=>x.CommentTime).ToList();

            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].IsPublic == false && model[i].CommentPerson != _user.UserAccout)
                {
                    model.Remove(model[i]);
                }
            }
            foreach (var item in model)
            {
                item.CommentsChildren = GetChild(item.Id);
            } 
            return PartialView("_GetAllComments",model);
        }
      
        public List<CommentViewModel> GetChild(int id)
        {

            var kids = _ctx.Comment.Where(k => k.ParentId == id && k.IsDelete == false).ToList();
            List<CommentViewModel> child = new List<CommentViewModel>();
            for (int i = 0; i < kids.Count; i++)
            {
                if (kids[i].IsPublic == false && kids[i].CommentPerson != _user.UserAccout)
                {
                    kids.Remove(kids[i]);
                }
            }
            foreach (var items in kids)
            {
                var c = _ctx.Comment.SingleOrDefault(m => m.Id == items.ParentId);
                
                CommentViewModel kid = new CommentViewModel();
                {
                    kid.Id = items.Id;
                    kid.ParentId = items.ParentId;
                    kid.CommentPerson = items.CommentPerson;
                    kid.NewsId = items.NewsId;
                    if (c != null)
                    {
                        if (c.CommentContent.Length < 20)
                        {
                            kid.CommentContent = items.CommentContent + "//回复@用户" + c.CommentPerson + ":" + c.CommentContent;
                        }
                        else
                        {
                            kid.CommentContent = items.CommentContent + "//回复@用户" + c.CommentPerson + ":" + c.CommentContent.Substring(0, 20);
                        }
                    }
                    else
                    {
                        kid.CommentContent = items.CommentContent;
                    }
                    kid.CommentTime = items.CommentTime;
                    kid.CommentsChildren = GetChild(kid.Id);
                }
                child.Add(kid);
            }
            return child;
        }
    }
}