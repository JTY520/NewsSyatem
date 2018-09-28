using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Web.Areas.Admin.Service.Authorization
{
    public class PermissionManager : IPermissionManager
    {
        public IEnumerable<Permission> GetAllPermissions()
        {
            return PermissionProvider._permissions;
        }
    }
}