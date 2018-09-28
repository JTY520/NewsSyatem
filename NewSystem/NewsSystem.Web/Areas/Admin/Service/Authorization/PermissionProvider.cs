using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Service.Authorization
{
    public class PermissionProvider
    {
        public static readonly List<Permission> _permissions = new List<Permission>
        {
            new Permission{ Code = PermissionCode.Total,DisplayName = "所有权限"},
            new Permission{ Code = PermissionCode.App, DisplayName = "后台操作权限"},
            #region 用户
            new Permission{ Code = PermissionCode.User, DisplayName = "用户"},
            #endregion
            #region 角色
            new Permission{ Code = PermissionCode.Role, DisplayName = "角色"}
            #endregion
        };
    }
}