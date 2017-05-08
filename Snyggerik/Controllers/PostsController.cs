using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Snyggerik.Models;

namespace Snyggerik.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult EditWithTags()
        {
            return View();
        }

        public ActionResult MyPosts()
        {
            var posts = db.Posts.Where(b => b.Blog.User.UserName == User.Identity.Name).ToList();

            return View(posts.ToList());
        }

        // GET: Posts
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create(int? blogId)
        {
            if (blogId != null || blogId != 0)
            {
                var userName = User.Identity.Name;

                var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();

                var blog = db.Blogs.Find(blogId);

                if (user == blog.User)
                {
                    Adel A = new Adel();
                    A.AllTags = db.Tags.ToList();

                    ViewBag.blogId = blogId;
                    Post p = new Post();
                    p.Blog = blog;

                    A.ThePost = new Post();
                    A.ThePost = p;
                    return View(A);
                }

            }
            return RedirectToAction("Index");

        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Adel A, int blogId, string taglist)
        {

            A.ThePost.PostCreated = DateTime.Now.Date;
            A.ThePost.Blog = db.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
            // OBS KOLLA NULL
            if (taglist != null && taglist!="")
            {
                taglist = taglist.Substring(0, taglist.Length - 1);
                List<string> arr = taglist.Split(',').ToList();
                for (int i = 0; i < arr.Count; i++)
                {
                    Tag T = new Tag();
                    int arrId = Int32.Parse(arr[i]);
                    PostTag PT = new PostTag();
                    PT.Post = A.ThePost;
                    PT.Tag = db.Tags.Where(x => x.TagId == arrId).FirstOrDefault();
                    db.PostTags.Add(PT);
                }
            }

            db.Posts.Add(A.ThePost);
            db.SaveChanges();


            return RedirectToAction("MyPosts");
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            //call from post detail
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);

            Adel adel = new Adel(post);
            //get all tags
            adel.AllTags = db.Tags.ToList();
            if (post == null)
            {
                return HttpNotFound();
            }
            //get selected tags
            adel.SelectedTags = post.PostTags.ToList();
            return View(adel);

        }

        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }
            post.Views++;
            db.SaveChanges();
            return View(post);
        }
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ///THIS IS THE ONE ///////////////////////////////////
        public ActionResult Edit(Adel AL, string taglist)
        {
            Post post = db.Posts.Where(x => x.IdPost == AL.ThePost.IdPost).FirstOrDefault();

            post.PostBody = AL.ThePost.PostBody;
            db.SaveChanges();


            //Delete all tags related to the post
            List<PostTag> pt = db.PostTags.Where(x => x.Post.IdPost == AL.ThePost.IdPost).ToList();
            for (int i = 0; i < pt.Count; i++)
            {
                db.PostTags.Remove(pt[i]);
            }
            //Remove comma
            taglist = taglist.Substring(0, taglist.Length - 1);
            List<string> arr = taglist.Split(',').ToList();
            //Add new tags
            for (int i = 0; i < arr.Count; i++)
            {
                Tag theTag = db.Tags.Find(Int32.Parse(arr[i]));
                PostTag T = new PostTag();
                T.Tag = theTag;
                T.Post = post;
                db.PostTags.Add(T);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            var comments = db.Comments.Where(x => x.Post.IdPost == id).ToList();
            var posttags = db.PostTags.Where(x => x.Post.IdPost == id).ToList();
            foreach (var c in comments)
            {
                db.Comments.Remove(c);
            }
            foreach (var pt in posttags)
            {
                db.PostTags.Remove(pt);
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("MyPosts");
        }


        public ActionResult FilterPosts()
        {
            SuperPosts SP = new SuperPosts();
            List<Tag> alltags = db.Tags.ToList();
            foreach (var t in alltags)
            {
                int c = 0;
                foreach (var pt in t.PostTags)
                {
                    c += pt.Post.Views;
                }
                SuperTag T = new SuperTag();
                T.TheTag = t;
                T.Used = c;
                SP.AllTags.Add(T);
            }
            List<Post> P = db.Posts.ToList();
            SP.Posts = P;
            SP.AllTags = SP.AllTags.OrderByDescending(o => o.Used).ToList();
            return View(SP);
        }

        [HttpPost]
        public ActionResult FilterPosts(string filter)
        {
            SuperPosts SP = new SuperPosts();

            List<Tag> alltags = db.Tags.ToList();
            foreach (var t in alltags)
            {
                int c = 0;
                foreach (var pt in t.PostTags)
                {
                    c += pt.Post.Views;
                }
                SuperTag T = new SuperTag();
                T.TheTag = t;
                T.Used = c;
                SP.AllTags.Add(T);
            }

            SP.AllTags = SP.AllTags.OrderByDescending(o => o.Used).ToList();
            List<Post> P = db.Posts.ToList();
            SP.Posts = new List<Post>();
            if (filter != "" && filter != null)
            {
                filter = filter.Substring(0, filter.Length - 1);
                List<string> arr = filter.Split(',').ToList();
                for (int i = 0; i < arr.Count; i++)
                {
                    SP.SearchTags.Add(db.Tags.Find(Int32.Parse(arr[i])));
                }
                foreach (var p in P)
                {
                    for (int i = 0; i < arr.Count; i++)
                    {
                        int id = Int32.Parse(arr[i]);
                        var posttag = db.PostTags.Where(x => x.Post.IdPost == p.IdPost && x.Tag.TagId == id).FirstOrDefault();
                        if (posttag != null)
                        {
                            SP.Posts.Add(p);
                            Tag tag = db.Tags.Where(x => x.TagId == posttag.Tag.TagId).FirstOrDefault();
                            SP.SearchTags.Add(tag);
                        }
                    }
                }
            }
            else
            {
                SP.Posts = P;
            }
            return View(SP);
        }

        [HttpGet]
        public ActionResult ShowTimeListPosts(int year, int month,int blogId)
        {
            List<Post> P = new List<Post>();

            var posts = db.Posts.Where(x => x.Blog.BlogId == blogId).ToList();

            foreach(var p in posts)
            {
                string dtStr = p.PostCreated.ToString();
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(dtStr);
                if(dt.Month==month && dt.Year == year)
                {
                    P.Add(p);
                }
            }


            return View(P);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
