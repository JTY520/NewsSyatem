using NewsSystem.Core.Users;
using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.Users
{
    public class UserAppService:IUserAppService
    {
        private readonly UserManger _userManager = new UserManger();
        //业务逻辑
        public User AddUser(string userAccount, string password, Nullable<bool> isAdmin)
        {
                return  _userManager.AddUser(userAccount,password, isAdmin);
        }
        public User Login(string userAccount, string password)
        {
            return _userManager.CheckLogin(userAccount, password);
        }

        public User DeletUser(string account)
        {
            return _userManager.DeletUser(account);
        }

        public User UpdateToCommon(string account)
        {
            return _userManager.UpdateToCommon(account);
        }

        public User UpdateToAdmin(string account)
        {
            return _userManager.UpdateToAdmin(account);
        }
    }
}
