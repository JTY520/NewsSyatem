using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Models
{
    public class CommentViewModel
    {
        public string NewsName { get; set; }
        public string CommentPerson { get; set; }
        public string CommentContent { get; set; }
        public System.DateTime CommentTime { get; set; }
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsPublic { get; set; }
        public List<CommentViewModel> CommentsChildren { get; set; }
        public int NewsId { get; set; }
        public string ReplyPersonName { get; set; }
    }
}