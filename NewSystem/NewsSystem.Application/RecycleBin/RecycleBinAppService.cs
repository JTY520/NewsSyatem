using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsSystem.EntityFramwork;
using NewsSystem.Core.RecycleBin;

namespace NewsSystem.Application.RecycleBin
{
    class RecycleBinAppService : IRecycleBinAppService
    {
       // private readonly NewsManager _newsManager = new NewsManager();
        private readonly RecycleBinManage _recycleBinManage = new RecycleBinManage();
        public Comment CommentCompletelyDelect(int id)
        {
            return _recycleBinManage.CommentCompletelyDelect(id);
        }
        
        public Comment CommentRecover(int id)
        {
            return _recycleBinManage.CommentRecover(id);
        }

        public News NewsCompletelyDelete(int id)
        {
            return _recycleBinManage.NewsCompletelyDelete(id);
        }

        public News NewsRecover(int id)
        {
            return _recycleBinManage.NewsRecover(id);
        }

        public User UserCompletelyDelete(string account)
        {
            return _recycleBinManage.UserCompletelyDelete(account);
        }

        public User UserRecover(string account)
        {
            return _recycleBinManage.UserRecover(account);
        }
    }
}
