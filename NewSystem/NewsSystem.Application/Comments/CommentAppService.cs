using NewsSystem.Core.Comments;
using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.Comments
{
    public class CommentAppService : ICommentAppService
    {
        private readonly CommentManage _commentManager = new CommentManage();

        public Comment DeleteComment(int id)
        {
            return _commentManager.DeleteComment(id);
        }

        public Comment PublicComment(int id)
        {
            return _commentManager.PublicComment(id);
        }

        public Comment BackOutComment(int id)
        {
            return _commentManager.BackOutComment(id);
        }
        public Comment WriteComment(int NewsId, User user, string comment, Nullable<int> parentId)
        {
            return _commentManager.WriteComment(NewsId, user, comment, parentId);
        }
    }
}
