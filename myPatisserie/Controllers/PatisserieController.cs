using DataAccessLayer;
using DataAccessLayer.MyEntities;
using myPatisserie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myPatisserie.Controllers
{
    public class PatisserieController : Controller
    {
        // GET: Patisserie
        public ActionResult Index()
        {
            //if (Session["UserName"] == null)
            //    return RedirectToAction("Login", "Home");

            return View();
        }

        public ActionResult Detail(int id)
        {

            Model1 ctx = new Model1();
            Cake entity = ctx.Cakes.Where(c => c.Id == id).FirstOrDefault();

            CakeViewModel model = new CakeViewModel();

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Price = entity.Price;
            model.ImageUrl = entity.ImageUrl;
            model.Comments = new List<CommentViewModel>();

            foreach (var item in entity.Comments)
            {
                CommentViewModel cmModel = new CommentViewModel();
                cmModel.Id = item.Id;
                cmModel.CommentStr = item.CommentStr;
                cmModel.User = item.User;
                model.Comments.Add(cmModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddComment(int CakeId,string CommentStr,int UserId)
        {
            Model1 ctx = new Model1();

            Comment cmntEntity = new Comment();
            cmntEntity.CommentStr = CommentStr;
            cmntEntity.User = ctx.Users.Find(UserId);
            cmntEntity.Cake = ctx.Cakes.Find(CakeId);
            
            ctx.Comments.Add(cmntEntity);
            ctx.SaveChanges();
            return RedirectToAction("Detail", new { id=CakeId});
        }

        public ActionResult Cakes()
        {
            List<Cake> patList = new List<Cake>();

            Model1 ctx = new Model1();

            patList = ctx.Cakes.ToList();

            return View(patList);
        }

        public ActionResult AddCakes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCakes(CakeViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            Cake cakeDataModel = new Cake();
            cakeDataModel.Id = model.Id;
            cakeDataModel.Name = model.Name;
            cakeDataModel.Price = model.Price;
            cakeDataModel.Description = model.Description;
            
            Model1 ctx = new Model1();
            ctx.Cakes.Add(cakeDataModel);
            ctx.SaveChanges();

            string fileName = Server.MapPath("../images/"+model.Name+"_"+ model.Id + ".jpg");

            var file = Request.Files["myimage"];

            file.SaveAs(fileName);

            model.ImageUrl = model.Name + "_" + model.Id + ".jpg";
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}