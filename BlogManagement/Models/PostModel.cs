using BlogManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogManagement.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public String Title { get; set; }
        public int AccountId { get; set; }
        public String UserName { get; set; }
        public DateTime DatePost { get; set; }
        public String Content { get; set; }
        public String Image { get; set; }
        public int Likes { get; set; }
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public String AccountImage { get; set; }
    }
}