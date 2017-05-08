using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    
    public class SuperPosts
    {

        public List<Post> Posts { get; set; }
        public List<SuperTag> AllTags { get; set; }
        public List<Tag> SearchTags { get; set; }
        public SuperPosts() {
            SearchTags = new List<Tag>();
            AllTags = new List<SuperTag>();
        }

    }
}