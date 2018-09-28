using NewsSystem.Application.NewsNews;
using NewsSystem.EntityFramwork;
using NewsSystem.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly NewsAppService _newsService = new NewsAppService();

        public ActionResult GetNewsById(int id)
        {
            ViewBag.newsId = id;
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
                                             CommentNumber=m.CommentNumber
                                         }).ToList();
            foreach(var m in model)
            {
                m.AllImagesUrl = _newsService.GetAllImgUrl(m.Content);
            }
            return View(model);
        }
        public ActionResult GetHotImgsToday()
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
            return PartialView("_GetHotImgsToday",model);
        }
        public ActionResult GetNewsByType(string type, int? page)
        {
                ViewBag.Type = type;
                int pageNumber = page ?? 1;
                int pageSize = 5;
                List<NewsViewModel> model = (from m in _ctx.News
                                             where (m.FirstType.Contains(type) == true || m.SecondType.Contains(type) == true) && m.IsPublish == true
                                             select new NewsViewModel
                                             {
                                                 Id = m.Id,
                                                 Title = m.Title,
                                                 Content = m.Content,
                                                 FirstType = m.FirstType,
                                                 SecondType = m.SecondType,
                                                 Writer = m.Writer,
                                                 CheckImage = m.CheckImage,
                                                 UpTime = m.UpTime
                                             }).OrderByDescending(x => x.UpTime).ToList();
                foreach (var item in model)
                {
                    if (item.CheckImage == true)
                    {
                        item.FirstImage = _newsService.GetFirstImage(item.Content);
                    }
                }
                IPagedList<NewsViewModel> pagedlist = model.ToPagedList(pageNumber, pageSize);
                return View(pagedlist);
        }
        [HttpGet]
        public ActionResult GetNewsBySearch(string str, int? page)
        {
            ViewBag.SeachName = str;
            int pageNumber = page ?? 1;
            int pageSize = 5;
            List<NewsViewModel> model = (from m in _ctx.News
                                         where (m.Title.Contains(str) == true || m.Content.Contains(str) == true) && m.IsPublish == true
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             Content = m.Content,
                                             FirstType = m.FirstType,
                                             SecondType = m.SecondType,
                                             Writer = m.Writer,
                                             CheckImage = m.CheckImage,
                                             UpTime = m.UpTime
                                         }).OrderByDescending(x => x.UpTime).ToList();
            foreach (var item in model)
            {
                if (item.CheckImage == true)
                {
                    item.FirstImage = _newsService.GetFirstImage(item.Content);
                }
            }
            IPagedList<NewsViewModel> pagedlist = model.ToPagedList(pageNumber, pageSize);
            return View(pagedlist);
        }
        [HttpPost]
        public ActionResult GetNewsBySearch(string str)
        {
            ViewBag.SeachName = str;
            int pageNumber = 1;
            int pageSize = 5;
            List<NewsViewModel> model = (from m in _ctx.News
                                         where (m.Title.Contains(str) == true || m.Content.Contains(str) == true) && m.IsPublish == true
                                         select new NewsViewModel
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             Content = m.Content,
                                             FirstType = m.FirstType,
                                             SecondType = m.SecondType,
                                             Writer = m.Writer,
                                             CheckImage = m.CheckImage,
                                             UpTime = m.UpTime
                                         }).OrderByDescending(x => x.UpTime).ToList();
            foreach (var item in model)
            {
                if(item.CheckImage==true)
                {
                       item.FirstImage = _newsService.GetFirstImage(item.Content);
                }
            }
            IPagedList<NewsViewModel> pagedlist = model.ToPagedList(pageNumber, pageSize);
            return View(pagedlist);
        }
        //[HttpPost]
        //public ActionResult GetNewsByKeyWordType(string keyWord,string range,int ? page)
        //{
        //    ViewBag.SeachName = keyWord;
        //    int pageNumber = page ?? 1;
        //    int pageSize = 5;
        //    List<NewsViewModel> model = (from m in _ctx.News
        //                                 where (m.Title.Contains(keyWord) == true || m.Content.Contains(keyWord) == true) && m.IsPublish == true
        //                                 select new NewsViewModel
        //                                 {
        //                                     Id = m.Id,
        //                                     Title = m.Title,
        //                                     Content = m.Content,
        //                                     FirstType = m.FirstType,
        //                                     SecondType = m.SecondType,
        //                                     Writer = m.Writer,
        //                                     CheckImage = m.CheckImage,
        //                                     UpTime = m.UpTime
        //                                 }).OrderByDescending(x => x.UpTime).ToList();
        //    foreach (var item in model)
        //    {
        //        item.FirstImage = _newsService.GetFirstImage(item.Content);
        //    }
        //    IPagedList<NewsViewModel> pagedlist = model.ToPagedList(pageNumber, pageSize);
        //    return View(pagedlist);
        //}
    }
}