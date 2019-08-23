using DataAccessLayer;
using myPatisserie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myPatisserie.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
  
            Session["UserName"] = "yzc1";
            Session["UserId"] = 1;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["userName"] = null;
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginItem lg)
        {
            Model1 ctx = new Model1();

            var loggedUSer = ctx.Users.Where(c => c.UserName == lg.UserName && c.Password == lg.Password).FirstOrDefault();

            if (loggedUSer == null)
            {
                ViewBag.hatali = 1;
                return View();
            }
            else
            {

                Session["userName"] = loggedUSer.UserName;
                Session["UserId"] = loggedUSer.Id;
                return RedirectToAction("Index", "Patisserie");
            }
        }
    }
}