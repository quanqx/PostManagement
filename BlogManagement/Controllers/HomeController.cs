using BlogManagement.BLL;
using BlogManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            AccountBLL account = new AccountBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
            PostBLL post = new PostBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
            ViewBag.lst = post.getAll();
            return View();
        }

        public ActionResult navPartial()
        {
            return PartialView();
        }
        public ActionResult NavLeftPartial()
        {
            CategoryBLL category = new CategoryBLL(new DAL.UnitOfWork.UnitOfWork(new DAL.Entities.BlogDBContext()));
            ViewBag.lstCategory = category.getAll();
            return PartialView();
        }
        public ActionResult CreatePostPartial()
        {
            ViewBag.CategoryId = new SelectList(new BlogDBContext().Categories, "CategoryId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreatePostPartial(Post model)
        {
            AccountBLL account = new AccountBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
            PostBLL post = new PostBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
            model.AccountId = account.getByEmail(User.Identity.Name).AccountId;
            model.CategoryId = 1;
            model.DatePost = DateTime.Now;
            post.Add(model);
            //var strContent = form["txtContent"].ToString();//FormCollection form
            return Redirect("Index");
        }


    }
}