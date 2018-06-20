using BlogManagement.BLL;
using BlogManagement.DAL.Entities;
using BlogManagement.DAL.UnitOfWork;
using BlogManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogManagement.Controllers
{
    public class ProfileController : Controller
    {
        private PostBLL postBLL;

        public ProfileController()
        {
            postBLL = new PostBLL(new UnitOfWork(new BlogDBContext()));
        }

        // GET: Profile
        public ActionResult TimeLine()
        {
            IEnumerable<PostModel> lstPost = postBLL.getPostModel();
            Dictionary<int, CommentModel> dic = new Dictionary<int, CommentModel>();
            foreach(var item in lstPost)
            {
                foreach(var i in item.Comments)
                {
                    CommentModel cmtModel = new CommentModel(i.CommentId, i.AccountId, i.Content, i.CommentTime, i.PostId, postBLL.getUserNameById(i.AccountId));
                    dic.Add(item.PostId, cmtModel);
                }
            }
            ViewBag.lstComment = dic;
            return View(lstPost);
        }
    }
}