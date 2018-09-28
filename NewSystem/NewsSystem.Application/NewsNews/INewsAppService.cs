using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.NewsNews
{

    public interface INewsAppService
    {
        News AddNews(string content,string title,string firstType,string secondType,string writer);
        News UpdateNews(int id, string content, string title, string firstType, string secondType, string writer);
        List<string> GetNewsImages(string html);
        News DeletNews(int id);
        News NewsPublic(int id);
        News NewsBackOut(int id);
        List<string> GetAllImgUrl(string html);
        string GetFirstImage(string html);

        meun AddNewsType(string newsType, Nullable<int> parentId);

        meun UpdateNewsType(int id,Nullable<int> parentId, string newType);

        meun DeleteNewsType(int id);

        string GetNewsTypeParentName(Nullable<int> parentId);

        bool NewsTypeExitChildren(int id);
         meun GetNewsType(int id);
    }
}
