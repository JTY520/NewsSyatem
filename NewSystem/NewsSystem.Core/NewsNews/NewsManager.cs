using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewsSystem.Core.NewsNews
{
    public class NewsManager
    {
        private readonly MESDbContext _ctx = new MESDbContext();
        ////增删改查
        public News AddNews(string content, string title, string firstType, string secondType, string writer)
        {
            List<string> imgurl = GetImages(content);
            News news = new News
            {
                CommentNumber = 0,
                Content = content,
                FirstType =firstType,
                SecondType = secondType,
                UpTime = DateTime.Now,
                Title = title,
                Writer =writer,
                IsDelet = false,
                IsPublish=false
            };
            if (imgurl.Count == 0)
            {
                news.CheckImage = false;
            }
            else
            {
                news.CheckImage = true;

            }
            var findNews = _ctx.News.SingleOrDefault(f => f.Title == title);
            if (findNews != null)
            {
                throw new Exception("该标题已经被上传");
            }
            else
            {
                _ctx.News.Add(news);
                _ctx.SaveChanges();
                return news;
                //throw new Exception("上传新闻成功，正在等待审核");
            }
        }
        public List<string> GetImages(string html)
        {
            List<string> list = new List<string>();
            Regex text = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
            string f1 = @html;
            foreach (Match item in text.Matches(f1))
            {
                if (item.Success)
                {
                    list.Add(item.Value);
                }
            }
            return list;
        }
        public News DeletNews(int id)
        {
            News news = _ctx.News.Find(id);
            news.IsDelet = true;
            news.IsPublish = false;
            _ctx.SaveChanges();
            // throw new Exception("删除成功");
            return news; 
        }

        public News UpdateNews(int id, string content, string title, string firstType, string secondType, string writer)
        {
            if(content != null&&title!= null&&firstType!=null&&secondType!= null&&writer!= null)
            {
            News news = _ctx.News.Find(id);
            news.Title = title;
            news.Writer = writer;
            news.Content =content;
            news.FirstType = firstType;
            news.SecondType = secondType;
            List<string> images = GetImages(content);
            if (images.Count > 0)
                news.CheckImage = true;
            else
                news.CheckImage = false;
            _ctx.SaveChanges();
                return news;
            }
           else

            throw new Exception("修改不成功");
           
        }

        public List<string> GetAllImgUrl(string html)
        {
            List<string> allImgs = GetImages(html);
            List<string> allImgUrl = new List<string>();
            for(int i = 0; i < allImgs.Count;i++)
            {
                string firstImageUrl = allImgs[i];
                int srcIndex = firstImageUrl.IndexOf("src=\"");
                string strSrc = firstImageUrl.Substring(srcIndex);
                int firstIndex = strSrc.IndexOf("\"");
                int lastIndex = strSrc.IndexOf("\"", firstIndex + 1);
                string src = strSrc.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                allImgUrl.Add(src);
            }
            return allImgUrl;
        }
        public string GetFirstImages(string html)
        {
            List<string> results = GetImages(html);
            string firstImageUrl = results[0];
            int srcIndex = firstImageUrl.IndexOf("src=\"");
            string strSrc = firstImageUrl.Substring(srcIndex);
            int firstIndex = strSrc.IndexOf("\"");
            int lastIndex = strSrc.IndexOf("\"", firstIndex + 1);
            string src = strSrc.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            return src;

        }

        public  News NewsPublic(int id)
        {
            News news = _ctx.News.Find(id);
            news.IsPublish = true;
            _ctx.SaveChanges();
            return news;
        }

        public News NewsBackOut(int id)
        {
            News news = _ctx.News.Find(id);
            news.IsPublish = false;
            _ctx.SaveChanges();
            return news;
        }
        public meun AddNewsType(string newsType,Nullable<int> parentId)
        {
            meun list = _ctx.meun.SingleOrDefault(m=>m.Name==newsType&&m.ParentId==parentId);
            if (list == null)
            {
                meun m = new meun()
                {
                    Name = newsType,
                    ParentId = parentId,
                    IsDelete = false
                };
                _ctx.meun.Add(m);
                _ctx.SaveChanges();
                return m;
            }
            throw new Exception();
        }

        public meun UpdateNewsType(int id, Nullable<int> parentId, string newType)
        {
            try
            {
                meun m = _ctx.meun.SingleOrDefault(c => c.Id == id);
                m.Name = newType;
                m.ParentId = parentId;
                _ctx.SaveChanges();
                return m;
            }
            catch
            {
                throw new Exception("添加失败");
            }
           
        }

        public meun DeleteNewsType(int id)
        {
            meun m = _ctx.meun.SingleOrDefault(c => c.Id == id);
            m.IsDelete = true;
            _ctx.SaveChanges();
            return m;
        }

        public string GetNewsTypeParentName(Nullable<int> parentId)
        {
            if (parentId == null)
            {
                return null;
            }
            else
            {
                return _ctx.meun.SingleOrDefault(p => p.Id == parentId).Name;
            }
           
        }

        public bool NewsTypeExitChildren(int id)
        {
            meun type = _ctx.meun.Find(id);
            if (type.ParentId == null)
            {
                var list = _ctx.meun.Where(m => m.ParentId == id);
                if (list == null)
                {
                    return false;
                }
                return true;
            }
            return false;
           
        }

        public meun GetNewsType(int id)
        {
            return _ctx.meun.SingleOrDefault(m => m.Id == id);
        }
    }
}
