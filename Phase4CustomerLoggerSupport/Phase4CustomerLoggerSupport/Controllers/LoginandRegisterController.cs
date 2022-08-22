using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phase4CustomerLoggerSupport.Models;

namespace Phase4CustomerLoggerSupport.Controllers
{
    public class LoginandRegisterController : Controller
    {
        shaliniDBEntities db = new shaliniDBEntities();

        public ActionResult AddCustPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustPage(CustLogInfo custLogInfo)
        {
            if (db.CustLogInfoes.Any(x => x.CustName == custLogInfo.CustName))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.CustLogInfoes.Add(custLogInfo);
                db.SaveChanges();

                Session["LogIdSS"] = custLogInfo.LogId.ToString();
                Session["CustNameSS"] = custLogInfo.CustName.ToString();
                return RedirectToAction("Login", "Home");



            }

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserInfo userInfo)
        {
            var checkLogin = db.UserInfoes.Where(x => x.UserId.Equals(userInfo.UserId) && x.Password.Equals(userInfo.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["UserIdSS"] = userInfo.UserId.ToString();
                Session["PasswordSS"] = userInfo.Password.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong username or password";
            }
            return View();
        }
    }
}