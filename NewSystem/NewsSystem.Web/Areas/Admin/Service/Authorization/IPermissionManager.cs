using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Service.Authorization
{
    public interface IPermissionManager
    {
        /// <summary>
        /// 获取所有的权限
        /// </summary>
        /// <returns></returns>
        IEnumerable<Permission> GetAllPermissions();
    }
}