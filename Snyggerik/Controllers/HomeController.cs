using Snyggerik.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Snyggerik.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            //var x = 4;
            
            //x = 24*4;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Browse()
        {
            ViewBag.Message = "Your Browser page.";

            return View();
        }

        public ActionResult Search(int? id)
        {
            //return PartialView("Search");
            return View("Search");
        }

        public ActionResult LastPost()
        {
            var post = db.Posts.OrderByDescending(x => x.PostCreated).FirstOrDefault();
            return PartialView("_LastPost",post);
        }













    }
    



}
