using BlogManagement.BLL;
using BlogManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        //public ActionResult Index()
        //{
        //    ViewBag.CategoryId = new SelectList(new BlogDBContext().Categories, "CategoryId", "Name");
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(Post model)
        //{
        //    AccountBLL account = new AccountBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
        //    PostBLL post = new PostBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
        //    model.AccountId = account.getByEmail(User.Identity.Name).AccountId;
        //    model.CategoryId = 1;
        //    model.DatePost = DateTime.Now;
        //    post.Add(model);
        //    //var strContent = form["txtContent"].ToString();//FormCollection form
        //    return Redirect("Index");
        //}
        public String Image()
        {
            //Kiểm tra file upload có tồn tại không
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var fileImg = Request.Files["HelpSectionImages"];
                // xử lý của bạn
                // Bạn có thể trả về 1 json đường dẫn ảnh
                fileImg.SaveAs(Server.MapPath("~/Content/Image/" + fileImg.FileName));
                return "/Content/Image/" + fileImg.FileName;
            }
            return "";
        }
        public ActionResult MyProfile(int page = 1)
        {
            AccountBLL account = new AccountBLL(new DAL.UnitOfWork.UnitOfWork(new BlogDBContext()));
            PostBLL post = new PostBLL(new DAL.UnitOfWork.UnitOfWork(new DAL.Entities.BlogDBContext()));
            ViewBag.lst = post.getPostsByAccountId(account.getByEmail(User.Identity.Name).AccountId);
            return View();
        }
    }
}