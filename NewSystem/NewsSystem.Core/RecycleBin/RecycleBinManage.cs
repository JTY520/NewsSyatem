using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Core.RecycleBin
{
   public  class RecycleBinManage
    {
        private readonly MESDbContext _ctx = new MESDbContext();
        public Comment CommentCompletelyDelect(int id)
        {
            return null;
        }
        public Comment CommentRecover(int id)
        {
            Comment com = _ctx.Comment.SingleOrDefault(c => c.Id == id);
            com.IsDelete = false;
            com.IsPublic = false;
            _ctx.SaveChanges();
            return com;
        }

        public News NewsCompletelyDelete(int id)
        {
            throw new NotImplementedException();
        }

        public News NewsRecover(int id)
        {
            News news = _ctx.News.SingleOrDefault(c => c.Id == id);
            news.IsDelet = false;
            news.IsPublish = false;
            _ctx.SaveChanges();
            return news;
        }

        public User UserCompletelyDelete(string account)
        {
            throw new NotImplementedException();
        }

        public User UserRecover(string account)
        {
            User user = _ctx.User.SingleOrDefault(c => c.UserAccout == account);
            user.IsDelet = false;
            user.IsAdmin = false;
            _ctx.SaveChanges();
            return user;
        }
    }
}
