using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Service.Authorization
{
    public class PermissionCode
    {
        public const string Total = "Total";
        public const string App = Total + ".App";
        #region 用户
        public const string User = App + ".User";
        #endregion 
        #region 角色
        public const string Role = App + ".Role";
        public const string Editor = App + ".Editor"
        #endregion
       ;
    }
}