using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsSystem.EntityFramwork;
using NewsSystem.Core.NewsNews;

namespace NewsSystem.Application.NewsNews
{  
     public class NewsAppService : INewsAppService
    {
        private readonly NewsManager _newsManager = new NewsManager();
        public News AddNews(string content, string title, string firstType, string secondType, string writer)
        {
            return  _newsManager.AddNews( content, title, firstType, secondType, writer);
        }

        public News DeletNews(int id)
        {
           return  _newsManager.DeletNews(id);
        }

        public News UpdateNews(int id, string content, string title, string firstType, string secondType, string writer)
        {
            return _newsManager.UpdateNews(id, content, title, firstType, secondType, writer);
        }
        public List<string> GetNewsImages(string html)
        {
            return _newsManager.GetImages(html);
        }
        public List<string> GetAllImgUrl(string html)
        {
            return _newsManager.GetAllImgUrl(html);
        }
        public string GetFirstImage(string html)
        {
            return _newsManager.GetFirstImages(html);
        }

       public News NewsPublic(int id)
        {
            return _newsManager.NewsPublic(id);
        }

        public News NewsBackOut(int id)
        {
            return _newsManager.NewsBackOut(id);
        }
        public meun AddNewsType(string newsType, Nullable<int> parentId)
        {
            return _newsManager.AddNewsType(newsType,parentId);
        }

        public meun UpdateNewsType(int id, Nullable<int> parentId, string newType)
        {
            return _newsManager.UpdateNewsType(id,parentId,newType);
        }

        public meun DeleteNewsType(int id)
        {
            return _newsManager.DeleteNewsType(id);
        }
        public string GetNewsTypeParentName(Nullable<int> parentId)
        {
            return _newsManager.GetNewsTypeParentName(parentId);
        }

        public bool NewsTypeExitChildren(int id)
        {
            return _newsManager.NewsTypeExitChildren(id);
        }
        public meun GetNewsType(int id)
        {
            return _newsManager.GetNewsType(id);
        }
        
    }
}
