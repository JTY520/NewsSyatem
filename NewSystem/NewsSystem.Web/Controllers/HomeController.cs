
using NewsSystem.Application.NewsNews;
using NewsSystem.EntityFramwork;
using NewsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly NewsAppService _newsService = new NewsAppService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsType()
      {
            List<NewsTypeViewModel> model = (from m in _ctx.meun
                                             where m.ParentId == null&&m.IsDelete==false
                                             select new NewsTypeViewModel
                                             {
                                                 Id = m.Id,
                                                 Name = m.Name
                                             }).OrderBy(a=>a.Id).Take(9).ToList();
            return PartialView("_NewsType",model);
        }
       
        public ActionResult OtherNewsType()
        {
            List<NewsTypeViewModel> model = (from m in _ctx.meun
                                             where m.ParentId == null && m.IsDelete == false
                                             select new NewsTypeViewModel
                                             {
                                                 Id = m.Id,
                                                 Name = m.Name
                                             }).OrderBy(a => a.Id).Skip(10).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LunBoThreeImages()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false &&m.IsPublish==true
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(3).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item .Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_LunBo", model);
        }

        public ActionResult Recommend()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false && m.SecondType == "图片"
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(4).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item.Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_Recommend", model);
        }
        public ActionResult GunDongImages()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false &&m.SecondType=="图片"
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(7).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item.Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_GunDongImages", model);
        }
        public ActionResult NewsPart(string type)
        {

            return PartialView("_NewsPart");
        }
        public ActionResult SecondType()
        {
            List<NewsTypeViewModel> model = (from m in _ctx.meun
                                         where m.ParentId == null
                                         select new NewsTypeViewModel
                                         {
                                             Id = m.Id,
                                             ParentId = m.ParentId,
                                             Name = m.Name,
                                             Url = m.Url,
                                         }).ToList();
           
            for(int i = 0;i < model.Count; i++)
            {
                model[i].Children = GetSecondType(model[i].Id);
                if (model[i].Children == null)
                {
                    model.Remove(model[i]);
                }
            }

            return PartialView("_SecondType", model.Take(3));
        }   
        public ActionResult TypePart(string firstType, string secondType)
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.FirstType.Contains(firstType) == true && m.SecondType.Contains(secondType) == true&&m.CheckImage==true && m.IsPublish == true
                                         select new NewsViewModel
                                         {
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             Content =m.Content,
                                             CheckImage = false
                                         }).OrderByDescending(p => p.CommentNumber).Take(6).ToList();
            if (model.Count > 0)
            {
                model[0].CheckImage = true;
                model[0].FirstImageUrl = "/News/GetNews?id=" + model[0].Id;
                model[0].FirstImage = _newsService.GetFirstImage(model[0].Content);
                return PartialView("_TypePart", model);
            }
            else
                return PartialView("_TypePart", null);

        }
        public List<NewsTypeViewModel> GetSecondType(int id)
        {
            List<NewsTypeViewModel> meunList = (from m in _ctx.meun
                                            where m.ParentId == id
                                            select new NewsTypeViewModel
                                            {
                                                Id = m.Id,
                                                ParentId = m.ParentId,
                                                Name = m.Name,
                                                Url = m.Url,
                                            }).ToList();
            return meunList;
        }

        public ActionResult GetFootImages()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false && m.SecondType == "图片"
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(5).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item.Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_GetFootImages",model);
        }
        public ActionResult GetFootType()
        {
            List<NewsTypeViewModel> model = (from m in _ctx.meun
                                             where m.Name != "首页"&&m.ParentId==null
                                             select new NewsTypeViewModel
                                             {
                                                 Id = m.Id,
                                                 Name = m.Name,
                                             }).ToList();
            return PartialView("_GetFootType", model);
        }

        public ActionResult HotPoint()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(5).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item.Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_HotPoint", model);
        }

        public ActionResult PicCoquettish()
        {
            List<NewsViewModel> model = (from m in _ctx.News
                                         where m.CheckImage == true && m.IsDelet == false && m.SecondType == "图片"
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             CommentNumber = m.CommentNumber,
                                             CheckImage = m.CheckImage,
                                             Content = m.Content
                                         }).OrderByDescending(x => x.CommentNumber).Take(6).ToList();
            foreach (var item in model)
            {
                item.FirstImage = _newsService.GetFirstImage(item.Content);
                item.FirstImageUrl = "/News/GetNewsById?id=" + item.Id;
            }
            return PartialView("_PicCoquettish", model);
        }
    }
}