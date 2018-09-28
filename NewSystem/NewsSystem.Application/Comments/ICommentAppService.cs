using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.Comments
{
    public interface ICommentAppService
    {
        Comment WriteComment(int NewsId, User user, string comment, Nullable<int> parentId);
        Comment DeleteComment(int id);
        Comment BackOutComment(int id);
        Comment PublicComment(int id);

    }
}
