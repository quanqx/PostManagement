using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogManagement.DAL.Entities;
using BlogManagement.DAL.UnitOfWork;
using BlogManagement.Models;

namespace BlogManagement.BLL
{
    public class PostBLL : IPostBLL
    {
        private IUnitOfWork uow;

        public PostBLL(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Add(Post post)
        {
            uow.postRepository.Add(post);
            uow.Save();
        }

        public void Delete(Post post)
        {
            uow.postRepository.Delete(post);
            uow.Save();
        }

        public Post get(int id)
        {
            return uow.postRepository.get(id);
        }

        public IEnumerable<Post> getAll()
        {
            return uow.postRepository.getAll();
        }

        public void Update(Post post)
        {
            uow.postRepository.Update(post);
            uow.Save();
        }

        public IEnumerable<Post> getPostsByAccountId(int id)
        {
            return uow.postRepository.getAll().Where(x => x.AccountId == id);
        }

        public IEnumerable<Post> getPostLimit(int from, int to)
        {
            return uow.postRepository.getAll().Skip(from).Take(to);
        }
	
        public IEnumerable<Post> getPostByCategoryId(int id)
        {
            return uow.postRepository.getAll().Where(x => x.CategoryId == id);
        }
        public IEnumerable<Post> getPostByCategoryIdAndAccountId(int idCategory, int idAccoutn)
        {
            return uow.postRepository.getAll().Where(x => x.CategoryId == idCategory && x.AccountId == idAccoutn);
		}

        public IEnumerable<PostModel> getPostModel()
        {
            var lst = from post in uow.postRepository.getAll()
                      from account in uow.accountRepository.getAll()
                      from category in uow.categoryRepository.getAll()
                      where post.AccountId == account.AccountId && post.CategoryId == category.CategoryId
                      select new
                      {
                          post.PostId,
                          post.Title,
                          post.AccountId,
                          account.UserName,
                          post.DatePost,
                          post.Content,
                          post.Image,
                          post.Likes,
                          post.CategoryId,
                          CategoryName = category.Name,
                          post.Comments,
                          AccountImage = account.Image
                      };
            
            List<PostModel> res = new List<PostModel>();
            foreach(var item in lst)
            {
                PostModel post = new PostModel();
                post.AccountId = item.AccountId;
                post.AccountImage = item.AccountImage;
                post.CategoryId = item.CategoryId;
                post.CategoryName = item.CategoryName;

                IEnumerable<Comment> lstComment = uow.commentRepository.getAll().Where(a => a.PostId == item.PostId);

                post.Comments = lstComment;
                post.Content = item.Content;
                post.DatePost = item.DatePost;
                post.Image = item.Image;
                post.Likes = item.Likes;
                post.PostId = item.PostId;
                post.Title = item.Title;
                post.UserName = item.UserName;
                res.Add(post);
            }
            return res;
        }

        public String getUserNameById(int id)
        {
            return uow.accountRepository.getById(id).UserName;
        }
    }
}