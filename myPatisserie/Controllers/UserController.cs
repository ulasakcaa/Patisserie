using DataAccessLayer;
using DataAccessLayer.MyEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myPatisserie.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
                return RedirectToAction("Login", "Home");
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string filterName,string filterEmail)
        {
            var ctx = new Model1();

            List<User> result = ctx.Users.Where(c => c.UserName.Contains(filterName) && c.Email.Contains(filterEmail)).ToList();

            return View(result);
        }

        public ActionResult UserListPartial(string filterName, string filterEmail)
        {
            var ctx = new Model1();
            if (filterName == null)
                filterName = "";
            if (filterEmail == null)
                filterEmail = "";
            List<User> result = ctx.Users.Where(c => c.UserName.Contains(filterName) && c.Email.Contains(filterEmail)).ToList();
            if (filterEmail == "" && filterName == "")
                result = ctx.Users.ToList();

            return PartialView(result);
        }

        public PartialViewResult UsrPartialVol1(string filterName, string filterEmail)
        {
            var ctx = new Model1();
            if (filterName == null)
                filterName = "";
            if (filterEmail == null)
                filterEmail = "";
            List<User> result = ctx.Users.Where(c => c.UserName.Contains(filterName) && c.Email.Contains(filterEmail)).ToList();
            if (filterEmail == "" && filterName == "")
                result = ctx.Users.ToList();

            return PartialView(result);
        }

        public ViewResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User usr)
        {

                if (String.IsNullOrEmpty(usr.UserName))
                {
                    ViewBag.errorMessage = "username is required";
                    return View();
                }

                if (String.IsNullOrEmpty(usr.Password))
                {
                    ViewBag.errorMessage = "Password is required";
                    return View();
                }

                if (String.IsNullOrEmpty(usr.Email))
                {
                    ViewBag.errorMessage = "Email is required";
                    return View();
                }

            var ctx = new Model1();

            var existing = ctx.Users.Where(c => c.UserName == usr.UserName).ToList();
                if (existing.Count > 0)
                {
                    ViewBag.errorMessage = "username is exist";
                    return View();
                }
                ctx.Set<User>().Add(usr);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var ctx = new Model1();
            var model = ctx.Users.Where(c => c.Id == id).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        public ActionResult Update(User usr)
        {

            if(ModelState.IsValid==false)
            {
                return View(usr);
            }
            var ctx = new Model1();

            var model = ctx.Users.Where(c => c.Id == usr.Id).FirstOrDefault();

            ctx.Entry(model).CurrentValues.SetValues(usr);
           
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

      
        public ActionResult Delete(int id)
        {
            var ctx = new Model1();
            var usrToDelete = ctx.Users.Where(c => c.Id == id).FirstOrDefault();
            ctx.Users.Remove(usrToDelete);

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}