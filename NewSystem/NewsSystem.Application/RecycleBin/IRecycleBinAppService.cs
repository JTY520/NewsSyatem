using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.RecycleBin
{
    interface IRecycleBinAppService
    {
        //彻底删除
        News NewsCompletelyDelete(int id);
        Comment CommentCompletelyDelect(int id);
        User UserCompletelyDelete(string account);
        //恢复
        News NewsRecover(int id);
        Comment CommentRecover(int id);
        User UserRecover(string account);
    
    }
}
