using NewsSystem.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Core.Users
{
    public class UserManger
    {
        private readonly MESDbContext _ctx = new MESDbContext();

        public User AddUser(string userAccount, string password, Nullable<bool> isAdmin)
        {
            User user = _ctx.User.SingleOrDefault(u => u.UserAccout == userAccount);
            if (user == null)
            {
                User people = new User
                {
                    UserAccout = userAccount,
                    Password = password,
                    IsDelet = false
                };
                if(isAdmin == false|| isAdmin == null)
                {
                    people.IsAdmin = false;
                }
                else
                {
                    people.IsAdmin = true;
                }
                _ctx.User.Add(people);
                _ctx.SaveChanges();
                return user;
            }
            else
            {
                throw new Exception("该手机号已经注册");
            }
        }
      
        public User CheckLogin(string userAccount, string password)
        {
            User user = _ctx.User.SingleOrDefault(u => u.UserAccout == userAccount && u.Password == password);
            if (user != null)
            {
                if (user.IsDelet == true)
                {
                    throw new Exception();
                }
                return user;
            }
            else
                throw new Exception();
        }

        public User DeletUser(string account)
        {
            User user = _ctx.User.Find(account);
            user.IsDelet = true;
            user.IsAdmin = false;
            _ctx.SaveChanges();
            return user;
        }

        public User UpdateToCommon(string account)
        {
            User user = _ctx.User.Find(account);
            user.IsAdmin = false;
            _ctx.SaveChanges();
            return user;
        }

        public User UpdateToAdmin(string account)
        {
            User user = _ctx.User.Find(account);
            user.IsAdmin = true;
            _ctx.SaveChanges();
            return user;
        }
    }
}
