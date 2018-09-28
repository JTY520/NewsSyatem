using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FirstType { get; set; }
        public string SecondType { get; set; }
        public int CommentNumber { get; set; }
        public DateTime UpTime { get; set; }
        public string Writer { get; set; }
        public bool CheckImage { get; set; }
        public Nullable<bool> IsDelet { get; set; }
        public Nullable<bool> IsPublish { get; set; }
        public List<string> AllImages { get; set; }
        public string FirstImage { get; set; }
        public string FirstImageUrl { get; set; }
        public List<string> AllImagesUrl { get; set; }
    }
}