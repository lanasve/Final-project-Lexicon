using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Snyggerik.Models
{
    public class PostTag
    {
        [Key]
        [Display(Name = "Identificator")]
        public int PostTagId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Tag Tag { get; set;}
    }
}