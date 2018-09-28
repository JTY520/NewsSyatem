using NewsSystem.EntityFramwork;
using NewsSystem.Web.Areas.Admin.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsSystem.Application.Users;

namespace NewsSystem.Web.Areas.Admin.Controllers
{
    public class UserManageController : Controller
    {
        // GET: Account
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly UserAppService _userService = new UserAppService();
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
       // [Attribute]
        [HttpPost]
        public ActionResult AddUser(string userAccount,bool isAdmin,string password)
        {
            try
            {
                User user = _userService.AddUser(userAccount,password,isAdmin);
                return Json("添加成功", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("添加失败", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userAccount,string password)
        {
            try
            {
                User user = _userService.Login(userAccount, password);
                if (user.IsAdmin == true && user.IsDelet == false)
                {
                    Session.Add("Admin", user);
                    return Json("登陆成功", JsonRequestBehavior.AllowGet);
                }
               else
                {
                    return Json("只有管理员才能登陆",JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("密码错误或该帐号不存在", JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult UserDelete(string account)
        {
            User user = _userService.DeletUser(account);
            return RedirectToAction("GetAllUsers", "UserManage");
        }

        public ActionResult GetAllUsers()
        {
            User user = System.Web.HttpContext.Current.Session["Admin"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<UserViewModel> model = (from m in _ctx.User
                                             where m.IsDelet == false
                                             select new UserViewModel
                                             {
                                                 UserAccout = m.UserAccout,
                                                 IsAdmin = m.IsAdmin,
                                                 NickName = m.NickName
                                             }).ToList();
                return View(model);
            }

        }



        public ActionResult GetCommmonUser()
        {
            User user = System.Web.HttpContext.Current.Session["Admin"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<UserViewModel> model = (from m in _ctx.User
                             where m.IsDelet == false && m.IsAdmin == false
                             select new UserViewModel
                             {
                                 UserAccout = m.UserAccout,
                                 NickName=m.NickName
                             }).ToList();
                return View(model);
            }
        }

        public ActionResult GetAdmin()
        {
            User user = System.Web.HttpContext.Current.Session["Admin"] as User;
            if (user == null)
                return RedirectToAction("Login", "UserManage");
            else
            {
                List<UserViewModel> model = (from m in _ctx.User
                             where m.IsDelet == false && m.IsAdmin == true
                             select new UserViewModel
                             {
                                 UserAccout = m.UserAccout
                             }).ToList();
               return View(model);
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("Admin");
            return Json("注销成功", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateToCommon(string account)
        {
            User user = _userService.UpdateToCommon(account);
            return RedirectToAction("GetCommmonUser","UserManage");
        }

        public ActionResult UpdateToAdmin(string account)
        {
            User user = _userService.UpdateToAdmin(account);
            return RedirectToAction("GetAdmin", "UserManage");
        }

   
    }
}