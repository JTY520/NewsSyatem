using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Application.Users
{
    public interface IUserAppService
    {
        User Login(string userAccount, string password);
        User AddUser(string userAccount, string password, Nullable<bool> isAdmin);
        User DeletUser(string account);

        User UpdateToCommon(string account);

        User UpdateToAdmin(string account);
    }
}
