using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Core.Comments
{
    public class CommentManage
    {
        private readonly MESDbContext _ctx = new MESDbContext();

        public Comment WriteComment(int NewsId, User user, string comment, Nullable<int> parentId)
        {
            if (user == null)
                throw new Exception("请登陆后再评论");
            else if (comment == null)
                throw new Exception("评论不能为空");
            else if (user != null && comment != null)
            {

                Comment com = new Comment()
                {
                    NewsId = NewsId,
                    CommentContent = comment,
                    CommentPerson = user.UserAccout,
                    CommentTime = DateTime.Now,
                    ParentId = parentId,
                    IsDelete = false,
                    IsPublic=false
                };
                _ctx.Comment.Add(com);
                _ctx.SaveChanges();
                News news = _ctx.News.Find(NewsId);
                news.CommentNumber++;
                _ctx.SaveChanges();
                return com;
            }
            else
                throw new Exception("未知错误");

        }

        public Comment PublicComment(int id)
        {
            Comment com = _ctx.Comment.Find(id);
            com.IsPublic = true;
            _ctx.SaveChanges();
            return com;

        }

        public Comment BackOutComment(int id)
        {
            Comment com = _ctx.Comment.Find(id);
            com.IsPublic = false;
            _ctx.SaveChanges();
            return com;
        }
        public Comment DeleteComment(int id)
        {
            Comment com = _ctx.Comment.Find(id);
            com.IsPublic = false;
            com.IsDelete = true;
            _ctx.SaveChanges();
            return com;
        }
    }
}
