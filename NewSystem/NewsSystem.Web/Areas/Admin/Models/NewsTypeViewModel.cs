using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Models
{
    public class NewsTypeViewModel
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public List<NewsTypeViewModel> Chlidren { get; set; }

        public string ParentName { get; set; }
    }
}