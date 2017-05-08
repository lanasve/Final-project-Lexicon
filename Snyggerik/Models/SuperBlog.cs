using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    public class SuperBlog
    {
        public Blog Blog { get; set; }
        public int totalViews { get; set; }
        public List<Tag> Tags { get; set; }
        public List<TimeList> timelist { get; set; }
        public SuperBlog() { }
    }
}