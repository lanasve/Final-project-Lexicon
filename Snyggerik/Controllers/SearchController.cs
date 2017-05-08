using Snyggerik.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snyggerik.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        


        [HttpGet]
        public JsonResult GetData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);
            
            var blogs = db.Blogs.ToList();

            List<Blog> blogList = new List<Blog>();

            var lowerWord = keyword.ToLower();

            foreach (var blog in blogs)
            {
                
                if (blog.BlogTitle.ToLower().Contains(lowerWord) ||
                    blog.BlogBody.ToLower().Contains(lowerWord))

                {
                    var newBlog = new Blog();

                    newBlog.BlogId = blog.BlogId;
                    newBlog.BlogBody = blog.BlogBody;
                    newBlog.BlogTitle = blog.BlogTitle;
                    newBlog.BlogCreated = blog.BlogCreated;
                    

                    blogList.Add(newBlog);

                }

            }
            
            return Json(blogList, JsonRequestBehavior.AllowGet);

        }
        



        [HttpGet]
        public JsonResult GetTagData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);
            
            var tags = db.Tags.ToList();

            List<Tag> tagList = new List<Tag>();
            var lowerWord = keyword.ToLower();
            
            foreach (var tag in tags)
            {
                if (tag.Name.ToLower().Contains(lowerWord))

                {

                    var newTag = new Tag();                    
                    newTag.TagId = tag.TagId;
                    newTag.Name = tag.Name;
                   

                    tagList.Add(newTag);

                }
            }
       return Json(tagList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetPostData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);

            var posts = db.Posts.ToList();

            List<Post> postList = new List<Post>();
            var lowerWord = decodedKeyword.ToLower();


            foreach (var post in posts)
            {
                if (post.PostTitle.ToLower().Contains(lowerWord) || 
                    post.PostBody.ToLower().Contains(lowerWord))

                {

                    var newPost = new Post();

                    newPost.PostBody    =   post.PostBody;
                    newPost.PostTitle   =   post.PostTitle;                    
                    newPost.IdPost      =   post.IdPost;
                    newPost.PostCreated =   post.PostCreated;
                    newPost.Views = post.Views;


                    postList.Add(newPost);

                }
            }
            return Json(postList, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetPostTagData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);

            var posttags = db.PostTags.ToList();

            List<PostTag> postTagList = new List<PostTag>();
            var lowerWord = decodedKeyword.ToLower();


            foreach (var posttag in posttags)
            {
                if (posttag.Tag.Name.ToLower().Contains(lowerWord) )
                   
                {

                    var newPostTag = new PostTag();

                    newPostTag.PostTagId = posttag.PostTagId;
                    newPostTag.Tag = posttag.Tag;
                    newPostTag.Post = posttag.Post;
                 

                    postTagList.Add(newPostTag);

                }
            }
            return Json(postTagList, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Search(int? id)
        {

            

            return View("Search");
        }



        //public ActionResult FilterPosts()
        //{
        //    SuperPosts SP = new SuperPosts();
        //    List<Tag> alltags = db.Tags.ToList();
        //    foreach (var t in alltags)
        //    {
        //        int c = 0;
        //        foreach (var pt in t.PostTags)
        //        {
        //            c += pt.Post.Views;
        //        }
        //        SuperTag T = new SuperTag();
        //        T.TheTag = t;
        //        T.Used = c;
        //        SP.AllTags.Add(T);
        //    }
        //    List<Post> P = db.Posts.ToList();
        //    SP.Posts = P;
        //    SP.AllTags = SP.AllTags.OrderByDescending(o => o.Used).ToList();
        //    return View(SP);
        //}

        //[HttpPost]
        //public ActionResult FilterPosts(string filter)
        //{
        //    SuperPosts SP = new SuperPosts();

        //    List<Tag> alltags = db.Tags.ToList();
        //    foreach (var t in alltags)
        //    {
        //        int c = 0;
        //        foreach (var pt in t.PostTags)
        //        {
        //            c += pt.Post.Views;
        //        }
        //        SuperTag T = new SuperTag();
        //        T.TheTag = t;
        //        T.Used = c;
        //        SP.AllTags.Add(T);
        //    }

        //    SP.AllTags = SP.AllTags.OrderByDescending(o => o.Used).ToList();
        //    List<Post> P = db.Posts.ToList();
        //    SP.Posts = new List<Post>();
        //    if (filter != "" && filter != null)
        //    {
        //        filter = filter.Substring(0, filter.Length - 1);
        //        List<string> arr = filter.Split(',').ToList();
        //        for (int i = 0; i < arr.Count; i++)
        //        {
        //            SP.SearchTags.Add(db.Tags.Find(Int32.Parse(arr[i])));
        //        }
        //        foreach (var p in P)
        //        {
        //            for (int i = 0; i < arr.Count; i++)
        //            {
        //                int id = Int32.Parse(arr[i]);
        //                var posttag = db.PostTags.Where(x => x.Post.IdPost == p.IdPost && x.Tag.TagId == id).FirstOrDefault();
        //                if (posttag != null)
        //                {
        //                    SP.Posts.Add(p);
        //                    Tag tag = db.Tags.Where(x => x.TagId == posttag.Tag.TagId).FirstOrDefault();
        //                    SP.SearchTags.Add(tag);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        SP.Posts = P;
        //    }
        //    return View(SP);
        //}


    }
}
