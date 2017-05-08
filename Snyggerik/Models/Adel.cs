using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    public class Adel
    {
        public Post ThePost { get; set; }
        public List<Tag> AllTags { get; set; }
        public List<PostTag> SelectedTags { get; set; }
        public Adel() { }

        public Adel(Post P)
        {
            AllTags = new List<Tag>();
            ThePost=P;
        }
    }
}