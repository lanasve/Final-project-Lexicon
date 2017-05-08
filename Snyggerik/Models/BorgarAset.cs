using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{



    public class BorgarAset
    {

        public virtual Blog Blogs { get; set; }
        public virtual Comment Comments { get; set; }
        public virtual Post Posts { get; set; }
        public virtual PostTag PostTags { get; set; }
        public virtual Tag Tags { get; set; }



        public static List<Blog> AllBlogs { get; set; }
        public static List<Comment> AllComments { get; set; }
        public static List<Post> AllPosts { get; set; }
        public static List<PostTag> AllPostTags { get; set; }
        public static List<Tag> AllTags { get; set; }


        internal class List<Blog, Comment, Post, PostTag, Tag>
        {
            public virtual List<Blog, Comment, Post, PostTag, Tag> AllList { get; set; }
        }
        




        // public static Dictionary<Blog, Comment, Post, PostTag, Tag>

    }
   
}