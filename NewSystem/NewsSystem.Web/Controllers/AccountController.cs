using NewsSystem.Application.NewsNews;
using NewsSystem.Application.Users;
using NewsSystem.EntityFramwork;
using NewsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly MESDbContext _ctx = new MESDbContext();
        private readonly UserAppService _userService = new UserAppService();
        [HttpGet]
        public ActionResult Register()
        {
            return PartialView("_Register",null);
            //return View();
        }
        [HttpPost]
        public ActionResult Register(string userAccount,string password)
        {
            try
            {
                User user = _userService.AddUser(userAccount,password,false);
                return Json("注册成功", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("该帐号已注册", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return PartialView("_Login",null);
           // return View();
        }
        [HttpPost]
        public ActionResult Login(string userAccount, string password)
        {
            try
            {
                User user = _userService.Login(userAccount, password);
                    Session.Add("user", user);
                    return Json("登陆成功", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("登陆失败!帐号不存在或密码错误", JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Logout()
        {
            Session.Remove("user");
            return Json("注销成功", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Personally(string account)
        {
            return View();
        }
    }
}